using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data;
using WeatherMonitor2018.Data.Models;
using WeatherMonitor2018.UserControls;
using WeatherMonitorClassLibrary;

namespace WeatherMonitor2018.Pages
{
    public partial class StationPage : UserControl
    {
        public StationPage()
        {
            AddHandler(StationTab.RegionHeaderChangedEvent, new RoutedEventHandler(RegionHeaderChangedEventHandler));
            InitializeComponent();
            LoadTabs();
        }

        public void LoadTabs()
        {
            List<Region> regions = ReadDatabase.GetRegions();
            List<StationInfo> stations = ReadDatabase.GetStations();
            tab1.Content = new StationTab(regions, stations, 0);
            tab2.Content = new StationTab(regions, stations, 1);
            tab3.Content = new StationTab(regions, stations, 2);
            tab4.Content = new StationTab(regions, stations, 8);
            tab5.Content = new StationTab(regions, stations, 7);
        }

        private void RegionHeaderChangedEventHandler(object sender, RoutedEventArgs e)
        {
            string shortname = Utils.Truncate(e.OriginalSource.ToString(), 13);
            UpdateTabHeader(shortname);
        }

        private void UpdateTabHeader(string regionName)
        {
            switch (observationTabControl.SelectedIndex)
            {
                case 0:
                    tab1.Header = regionName;
                    break;
                case 1:
                    tab2.Header = regionName;
                    break;
                case 2:
                    tab3.Header = regionName;
                    break;
                case 3:
                    tab4.Header = regionName;
                    break;
                case 4:
                    tab5.Header = regionName;
                    break;
            }
        }

    }
}