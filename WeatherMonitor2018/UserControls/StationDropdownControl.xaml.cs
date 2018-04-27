using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data.Models;
using WeatherMonitorClassLibrary.Models.XmlResponses;
using WeatherMonitorClassLibrary.XmlService;

namespace WeatherMonitor2018.UserControls
{
    public partial class StationDropdownControl : UserControl
    {
        public IEnumerable<StationInfo> stationList;
        public StationInfo selectedStation;
        public StationDropdownControl()
        {
            InitializeComponent();
        }
        public static readonly RoutedEvent StationDropDownChangedEvent = EventManager.RegisterRoutedEvent(
            "RegionDropDownChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RegionDropdownControl));

        public event RoutedEventHandler StationDropDownChangedHandler
        {
            add { AddHandler(StationDropDownChangedEvent, value); }
            remove { RemoveHandler(StationDropDownChangedEvent, value); }
        }
        public void UpdateStationDropdown(int regionId)
        {
            var rows = stationList.Where(x => x.Region == regionId);
            Dropdown.ItemsSource = rows;
            Dropdown.SelectedIndex = 0;
        }
        private void StationSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) { return; }
            selectedStation = (e.AddedItems[0] as StationInfo);
            GetStationXML();
        }
        public void GetStationXML()
        {
            Station response = StationService.Get(selectedStation.StationNumber.ToString());
            RaiseEvent(new RoutedEventArgs(StationDropDownChangedEvent, response));
   
        }
    }
}
