using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.IO;

using Jil;

using tempestpoe.model;
using tempestpoe.viewmodel;

namespace tempestpoe
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    TempestTracker vm;
    //HttpClient client;
    //public ObservableCollection<Tempest> Tempests { get; set; }
    //Dictionary<string, string> tempestTable;
    //private Point mousePos;

    public MainWindow()
    {
      InitializeComponent();
      vm = new TempestTracker();
      this.DataContext = vm;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
      await vm.Init();
    }

    private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      string selected = (string)e.AddedItems[0];
      vm.UpdateFilter(selected);
    }

    private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.RightButton == MouseButtonState.Released)
      {
        this.DragMove();

      }
    }

    private void StackPanel_MouseDown_2(object sender, MouseButtonEventArgs e)
    {
      Application.Current.Shutdown();
    }

  }


}
