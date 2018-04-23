using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.Models;
using static WeatherMonitor2018.Data.WeatherMonitorDataSet;

namespace WeatherMonitor2018.Pages
{
    public partial class ObservationTab : UserControl
    {
        private ObservationService _observationService;
        private MapImageResolver _mapImageResolver;
        LandshlutarDataTable _landshlutaTable;
        StadirDataTable _stationTable;
        public ObservationTab(LandshlutarDataTable landshlutar, StadirDataTable stations, int selectedIndex)
        {
            _landshlutaTable = landshlutar;
            _stationTable = stations;
            InitializeComponent();
            _observationService = new ObservationService();
            _mapImageResolver = new MapImageResolver();
            landshlutaDropdown.ItemsSource = _landshlutaTable;
            landshlutaDropdown.SelectedIndex = selectedIndex;
        }
        public static readonly RoutedEvent LandshlutiChangedEvent = EventManager.RegisterRoutedEvent(
            "LandshlutiChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ObservationTab));
        public event RoutedEventHandler LandshlutiChanged
        {
            add { AddHandler(LandshlutiChangedEvent, value); }
            remove { RemoveHandler(LandshlutiChangedEvent, value); }
        }
        private void LandshlutiDropDownChange(object sender, SelectionChangedEventArgs e)
        {
            int landshlutaId = (e.AddedItems[0] as DataRowView).Row.Field<int>("Id");
            UpdateStationDropdown(landshlutaId);
            RaiseUpdateTabHeaderEvent(landshlutaId);
            ChangeImages();
        }
        private void RaiseUpdateTabHeaderEvent(int landshlutaId)
        {
            string name = (from r in _landshlutaTable
                           where r.Field<int>("Id") == landshlutaId
                            select r.Field<string>("Nafn")).First();
            RaiseEvent(new RoutedEventArgs(LandshlutiChangedEvent, name)); //Bubble Event to ObservationPageWrapper
        }
        private void StationSelect(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) { return; }
            string stationId = (e.AddedItems[0] as StadirRow).Field<int>("Stöðvanúmer").ToString();
            stationIdText.Text = "Stöðvanúmer: " + stationId;
            Station response = _observationService.GetStationObservation(stationId);
            nameTextBox.Text = response.Name;
            timiTextBox.Text = response.Time;
            hitiTextBox.Text = response.Hiti;
            vindstefnaTextBox.Text = response.Vindstefna;
            vindhradiTextBox.Text = response.Vindhradi;
            SetVedurlysingTextBox(response.Vedurlysing);
            CheckSationIndicator();
        }
        private void UpdateStationDropdown(int landshlutaId)
        {
            var rows = from row in _stationTable
                       where row.Spásvæði.Equals(landshlutaId)
                       select row;
            stationDropdown.ItemsSource = rows;
            stationDropdown.SelectedIndex = 0;
        }
        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ChangeImages()
        {
            allIndicatorsLayer.SetResourceReference(Image.SourceProperty, "empty");
            singleIndicatorLayer.SetResourceReference(Image.SourceProperty, "empty");
            string inverted = invertCheckbox.IsChecked == true ? "_inv" : String.Empty;
            string[] newMapLayers = _mapImageResolver.SetObservationMap(landshlutaDropdown.SelectedIndex);
            mapRootLayer.SetResourceReference(Image.SourceProperty, newMapLayers[0] + inverted);
            CheckIndicatorLayers(newMapLayers);
        }
        private void CheckIndicatorLayers(string[] newMapLayers)
        {
            if ( Utils.ArrayInBounds(1, newMapLayers) )
            {
                allIndicatorsLayer.SetResourceReference(Image.SourceProperty, newMapLayers[1]);
                CheckSationIndicator();
            };
        }
        private void CheckSationIndicator()
        {
            string indicator = _mapImageResolver.SetStationIndicator(landshlutaDropdown.SelectedIndex, stationDropdown.SelectedIndex);
            if (!string.IsNullOrEmpty(indicator))
            {
                singleIndicatorLayer.SetResourceReference(Image.SourceProperty, indicator);
            }
        }
        private void InvertCheckbox_Click(object sender, RoutedEventArgs e)
        {
            var active = mapRootLayer.GetValue(Image.SourceProperty).ToString();
            bool wasInverted = active.IndexOf("_invert", StringComparison.OrdinalIgnoreCase) >= 0;
            int startIndex = 4 + active.IndexOf("Map/", StringComparison.OrdinalIgnoreCase);
            if (invertCheckbox.IsChecked == true & !wasInverted)
            {
                active = active.Substring(startIndex, (active.Length - startIndex - 4)) + "_inv";
            }
            else if (invertCheckbox.IsChecked == false & wasInverted)
            {
                active = active.Substring(startIndex, (active.Length - startIndex - 11));
            }
            else
            {
                active = active.Substring(startIndex, (active.Length - startIndex - 4));
            }
            mapRootLayer.SetResourceReference(Image.SourceProperty, active);
        }
        private void SetVedurlysingTextBox(string vedurlysing)
        {
            if (vedurlysing.Trim() == string.Empty)
            {
                vedurlysingTextBox.Text = string.Empty;
                vedurlysingTextBox.Visibility = Visibility.Hidden;
            }
            else
            {
                vedurlysingTextBox.Text = vedurlysing;
                vedurlysingTextBox.Visibility = Visibility.Visible;
            }
        } 

    }
}