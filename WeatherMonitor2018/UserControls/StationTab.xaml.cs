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
using WeatherMonitor2018.Data.Models;
using WeatherMonitor2018.UserControls.SharedStationControls;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.Models.XmlResponses;

namespace WeatherMonitor2018.UserControls
{
    /// <summary>
    /// Interaction logic for StationTab.xaml
    /// </summary>
    public partial class StationTab : UserControl
    {
        private IEnumerable<Region> _regionList;

        public StationTab(IEnumerable<Region> regionList, IEnumerable<StationInfo> stationList, int selectedRegion)
        {
            InitializeComponent();
            AddHandler(RegionDropdownControl.RegionDropDownChangedEvent, new RoutedEventHandler(RegionDropDownChangedEventHandler));
            AddHandler(StationDropdownControl.StationLoadedEvent, new RoutedEventHandler(StationLoadedEventHandler));
            _regionList = regionList;
            stationDropdown.stationList = stationList;
            regionDropdown.Dropdown.ItemsSource = _regionList;
            regionDropdown.Dropdown.SelectedIndex = selectedRegion;
        }

        public static readonly RoutedEvent RegionHeaderChangedEvent = EventManager.RegisterRoutedEvent(
                    "RegionChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StationTab));
        private void RaiseUpdateTabHeaderEvent(int regionId)
        {
            string name = _regionList.Where(x => x.Id == regionId).Select(x => x.Name).First();
            RaiseEvent(new RoutedEventArgs(RegionHeaderChangedEvent, name)); // Bubble Event to StationPage
        }

        public event RoutedEventHandler RegionChangedHandler
        {
            add { AddHandler(RegionHeaderChangedEvent, value); }
            remove { RemoveHandler(RegionHeaderChangedEvent, value); }
        }
        private void RegionDropDownChangedEventHandler(object sender, RoutedEventArgs e)
        {
            int[] changes = e.OriginalSource as int[];
            string shortname = Utils.Truncate(e.OriginalSource.ToString(), 13);
            RaiseUpdateTabHeaderEvent(changes[0]);
            stationDropdown.UpdateStationDropdown(changes[0]);
            stationMap.ChangeImages(changes[1], stationDropdown.Dropdown.SelectedIndex);
        }
        private void StationLoadedEventHandler(object sender, RoutedEventArgs e)
        {
            Station response = e.OriginalSource as Station;
            stationResponse.FillTextBoxes(response);
            LoadStation();
        }
        private void LoadStation()
        {
            stationInfo.SetStationInfo(stationDropdown.selectedStation);
            stationMap.ChangeImages(regionDropdown.Dropdown.SelectedIndex, stationDropdown.Dropdown.SelectedIndex);
        }
        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            stationDropdown.GetStationXML();
        }

    }
}