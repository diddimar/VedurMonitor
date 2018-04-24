using System;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitorClassLibrary;
using WeatherMonitor2018.Data.WeatherMonitorDataSetTableAdapters;
using static WeatherMonitor2018.Data.WeatherMonitorDataSet;
using WeatherMonitorClassLibrary.ImageService;

namespace WeatherMonitor2018.Pages
{
    /// <summary>
    /// Interaction logic for ForecastPage.xaml
    /// </summary>
    public partial class ObservationTabControl : UserControl
    {
        public ObservationTabControl()
        {
            AddHandler(ObservationTab.LandshlutiChangedEvent, new RoutedEventHandler(LandshlutiChangedEventHandler));
            InitializeComponent();
            LandshlutarTableAdapter lta = new LandshlutarTableAdapter();
            StadirTableAdapter sta = new StadirTableAdapter();
            LandshlutarDataTable landshlutar = lta.GetData();
            StadirDataTable stations = sta.GetData();
            ObservationService os = new ObservationService();
            ObservationImageResolver oir = new ObservationImageResolver();
            LoadTabs(os, oir, landshlutar, stations);
            lta.Dispose();
            sta.Dispose();
            landshlutar.Dispose();
            stations.Dispose();
        }
        public void LoadTabs(
            ObservationService os, ObservationImageResolver oir,
            LandshlutarDataTable landshlutar, StadirDataTable stations)
        {
            tab1.Content = new ObservationTab(os, oir, landshlutar, stations, 0);
            tab2.Content = new ObservationTab(os, oir, landshlutar, stations, 1);
            tab3.Content = new ObservationTab(os, oir, landshlutar, stations, 2);
            tab4.Content = new ObservationTab(os, oir, landshlutar, stations, 8);
            tab5.Content = new ObservationTab(os, oir, landshlutar, stations, 7);
        }
        private void LandshlutiChangedEventHandler(object sender, RoutedEventArgs e)
        {
            string shortname = Utils.Truncate(e.OriginalSource.ToString(), 13);
            UpdateTabHeader(shortname);
        }
        private void UpdateTabHeader(string landshluti)
        {
            switch (observationTabControl.SelectedIndex)
            {
                case 0:
                    tab1.Header = landshluti;
                    break;
                case 1:
                    tab2.Header = landshluti;
                    break;
                case 2:
                    tab3.Header = landshluti;
                    break;
                case 3:
                    tab4.Header = landshluti;
                    break;
                case 4:
                    tab5.Header = landshluti;
                    break;
            }
        }

    }
}
