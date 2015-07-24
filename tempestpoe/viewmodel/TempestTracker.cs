using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tempestpoe.model;

using Jil;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Controls;

using eventsource;

namespace tempestpoe.viewmodel
{
  public class TempestTracker : INotifyPropertyChanged
  {
    private const string emptyMap = "If you enter this map please report which tempest is active";
    private const string unknownTempest = "Unknown Tempest";
    private const string noTempest = "None";

    public event PropertyChangedEventHandler PropertyChanged;
    public TempestList Tempests { get; set; }
    public List<string> TempestFilters { get; set; }
    public IEnumerable<string> Prefixes { get; set; }
    public IEnumerable<string> Suffixes { get; set; }

    private HttpClient client;
    public ObservableCollection<string> PrefixFavorites { get; set; }
    public ObservableCollection<string> SuffixFavorites { get; set; }


    public TempestTracker()
    {
      Prefixes = Tempest.Prefixes;
      Suffixes = Tempest.Suffixes;

      PrefixFavorites = new ObservableCollection<string>(UserPreference.Instance.Prefix);
      SuffixFavorites = new ObservableCollection<string>(UserPreference.Instance.Suffix);

      Tempests = new TempestList();
      client = new HttpClient();
      TempestFilters = new List<string>(){
        "show active",
        "show reported",
        "show all"
      };
    }

    private async Task ProcessUpdate(string body)
    {
      var temp = JSON.Deserialize<Dictionary<string, TempestDto>>(body);
      var tempests = new List<Tempest>();

      foreach (var map in temp.Keys)
      {
        var value = temp[map];

        var t = new Tempest()
        {
          Map = map,
          Name = value.name,
          Prefix = value.@base,
          Suffix = value.suffix
        };
        tempests.Add(t);
      }

      Tempests.UpdateTempest(tempests);
    }

    public async Task Init()
    {
      var es = new EventSource("http://localhost:5678/es");
      es.On("TEMPEST", ProcessUpdate);
      es.Listen();
    }

    public void UpdateFilter(string filter)
    {

      switch (filter)
      {
        case "show all":
          Tempests.Filter = (Tempest x) => { return true; };
          break;
        case "show active":

          Tempests.Filter = (Tempest x) =>
          {
            if (x.Name == unknownTempest || x.Name == noTempest)
            {
              return false;
            }
            
            return true;
          };
          break;

        case "show reported":
          Tempests.Filter = (Tempest x) =>
          {
            if (x.Name != unknownTempest)
            {
              return true;
            }
            return false;
          };
          break;
      }

    }


  }

  public class TempestList : List<Tempest>, INotifyCollectionChanged
  {
    public event NotifyCollectionChangedEventHandler CollectionChanged;

    private Dictionary<string, Tempest> _map;

    private Func<Tempest, bool> _filter;
    public Func<Tempest, bool> Filter
    {
      get
      {
        return _filter;
      }
      set
      {
        _filter = value;

        FilterTempests();
      }
    }

    public TempestList()
    {
      _map = new Dictionary<string, Tempest>();
      _filter = (Tempest x) =>
      {
        if (x.Name != "Unknown Tempest")
        {
          return true;
        }
        return false;
      };

    }

    public void UpdateTempest(Tempest tempest)
    {
      UpdateTempest(new[] { tempest });
    }

    public void UpdateTempest(IEnumerable<Tempest> tempests)
    {

      foreach (var tempest in tempests)
      {
        _map[tempest.Map] = tempest;
      }

      FilterTempests();

    }

    public void FilterTempests()
    {
      var maps = _map.Values.Where(x => Filter.Invoke(x));

      this.Clear();
      this.AddRange(maps);

      if (CollectionChanged != null)
      {
        CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }
    }

  }

  public class FilterableObserableCollection<G> : List<G>, INotifyCollectionChanged
  {

    private List<G> _storage;

    private Func<G, bool> _filter;
    public Func<G, bool> Filter
    {
      get
      {
        return _filter;
      }
      set
      {
        _filter = value;


        if (CollectionChanged != null)
        {
          CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
      }
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public FilterableObserableCollection()
    {
      _storage = new List<G>();
      this.Where(x => Filter.Invoke(x));
    }

    public void Add(G item)
    {
      _storage.Add(item);

    }


  }
}
