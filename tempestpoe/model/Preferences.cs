using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

using Jil;


namespace tempestpoe.model
{
  public class Preferences
  {
    public List<string> Prefix { get; set; }
    public List<string> Suffix { get; set; }

    public Preferences()
    {
      Prefix = new List<string>();
      Suffix = new List<String>();
    }

  }

  public static class UserPreference
  {
    public static Preferences Instance;

    static UserPreference()
    {
      if (!File.Exists("pref.json"))
      {
        Instance = new Preferences();
        using (var fs = File.Create("pref.json"))
        using(var writer = new StreamWriter(fs))
        {
          writer.Write(JSON.Serialize<Preferences>(Instance));
        }

      }
      else
      {
        var json = File.ReadAllText("pref.json");
        Instance = JSON.Deserialize<Preferences>(json);
      }


    }
  }
}
