using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data.Models;

namespace WeatherMonitor2018.UserControls.SharedStationControls
{
    public partial class RegionDropdownControl : UserControl
    {
        public RegionDropdownControl()
        {
            InitializeComponent();
        }
        public static readonly RoutedEvent RegionDropDownChangedEvent = EventManager.RegisterRoutedEvent(
            "RegionDropDownChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RegionDropdownControl));

        public event RoutedEventHandler RegionDropDownChangedEventHandler
        {
            add { AddHandler(RegionDropDownChangedEvent, value); }
            remove { RemoveHandler(RegionDropDownChangedEvent, value); }
        }
        private void RaiseRegionChangedEvent(int regionId, int selectedRegion)
        {
            int[] resp = new int[2] { regionId, selectedRegion };
            RaiseEvent(new RoutedEventArgs(RegionDropDownChangedEvent, resp )); //Bubble Event to StationTab
        }
        private void DropDownChange(object sender, SelectionChangedEventArgs e)
        {
            int regionId = (e.AddedItems[0] as Region).Id;
            RaiseRegionChangedEvent(regionId, Dropdown.SelectedIndex);
        }
    }
}
