using System;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data.WeatherMonitorDataSetTableAdapters;
using static WeatherMonitor2018.Data.WeatherMonitorDataSet;

namespace WeatherMonitor2018.Pages
{
    /// <summary>
    /// Interaction logic for ForecastPage.xaml
    /// </summary>
    public partial class ObservationPage : UserControl
    {
        public ObservationPage()
        {
            AddHandler(ObservationTab.LandshlutiChangedEvent, new RoutedEventHandler(LandshlutiChangedEventHandler));
            InitializeComponent();
            LandshlutarTableAdapter lta = new LandshlutarTableAdapter();
            StadirTableAdapter sta = new StadirTableAdapter();
            LandshlutarDataTable landshlutar = lta.GetData();
            StadirDataTable stations = sta.GetData();
            LoadTabs(landshlutar, stations);
            lta.Dispose();
            sta.Dispose();
            landshlutar.Dispose();
            stations.Dispose();
        }
        private void LandshlutiChangedEventHandler(object sender, RoutedEventArgs e)
        {
            Console.WriteLine( e.Source.ToString() );
            Console.WriteLine("LandshlutiChanged Event !");
        }

        public void LoadTabs(LandshlutarDataTable landshlutar, StadirDataTable stations)
        {
            spa1.Content = new ObservationTab(landshlutar, stations);
            spa2.Content = new ObservationTab(landshlutar, stations);
            spa3.Content = new ObservationTab(landshlutar, stations);
            spa4.Content = new ObservationTab(landshlutar, stations);
            spa5.Content = new ObservationTab(landshlutar, stations);
        }

    }
}
