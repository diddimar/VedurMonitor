using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.ImageService;
using WeatherMonitorClassLibrary.Models;
using static WeatherMonitor2018.Data.WeatherMonitorDataSet;

namespace WeatherMonitor2018.Pages
{
    public partial class ObservationTab : UserControl
    {
        private StadirRow _selectedStation;
        public static readonly RoutedEvent LandshlutiChangedEvent = EventManager.RegisterRoutedEvent(
            "LandshlutiChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ObservationTab));
        private ObservationService _observationService;
        private ObservationImageResolver _observationImageResolver;
        LandshlutarDataTable _landshlutaTable;
        StadirDataTable _stationTable;

        public ObservationTab(
            ObservationService observationService, ObservationImageResolver observationImageResolver,
            LandshlutarDataTable landshlutar, StadirDataTable stations, int selectedIndex)
        {
            InitializeComponent();
            _landshlutaTable = landshlutar;
            _stationTable = stations;
            _observationService = observationService;
            _observationImageResolver = observationImageResolver;
            landshlutaDropdown.ItemsSource = _landshlutaTable;
            landshlutaDropdown.SelectedIndex = selectedIndex;
        }
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
            //ChangeImages();
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

            _selectedStation = (e.AddedItems[0] as StadirRow);
            stationIdText.Text = "Stöðvanúmer: " + _selectedStation.Stöðvanúmer.ToString();
            LoadStation();
        }
        private void LoadStation()
        {
            Station response = _observationService.GetStationObservation(_selectedStation.Stöðvanúmer.ToString());
            FillTextBoxes(response);
            SetTextVisibility();
            SetStationDbInfo(_selectedStation);
            ChangeImages();
            CheckSationIndicator();
        }
        private void FillTextBoxes(Station response)
        {
            DateTime parsed;
            if (DateTime.TryParse(response.Time, out parsed))
                parsed = DateTime.Parse(response.Time);
            timiTextBox.Text = parsed.ToString("dd MMMM HH:MM");

            hitiTextBox.Text = response.Hiti;
            vindstefnaTextBox.Text = response.Vindstefna;
            vindhradiTextBox.Text = response.Vindhradi;
            mestiVindhradiTextBox.Text = response.MestiVindradi;
            vindhvidaTextBox.Text = response.MestaVindhvida;
            urkomaTextBox.Text = response.Urkoma;
            vedurlysingTextBox.Text = response.Vedurlysing;
            skyggniTextBox.Text = response.Skyggni;
            villaTextBox.Text = response.Err;
        }
        private void SetStationDbInfo(StadirRow selectedStation)
        {
            altitudeTextBox.Text = Convert.ToInt32(selectedStation.Hæð_yfir_sjó).ToString() + " metrar";
            ownerTextBox.Text = "Eigandi: " + selectedStation.Eigandi_stöðvar;
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
            LoadStation();
        }
        private void ChangeImages()
        {
            allIndicatorsLayer.SetResourceReference(Image.SourceProperty, "empty");
            singleIndicatorLayer.SetResourceReference(Image.SourceProperty, "empty");
            string inverted = invertCheckbox.IsChecked == true ? "_inv" : String.Empty;
            string[] newMapLayers = _observationImageResolver.GetObservationMap(landshlutaDropdown.SelectedIndex);
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
            string indicator = _observationImageResolver.SetStationIndicator(landshlutaDropdown.SelectedIndex, stationDropdown.SelectedIndex);
            if (!string.IsNullOrEmpty(indicator))
            {
                singleIndicatorLayer.SetResourceReference(Image.SourceProperty, indicator);
            }
        }
        private void InvertCheckbox_Click(object sender, RoutedEventArgs e)
        {
            var active = mapRootLayer.GetValue(Image.SourceProperty).ToString();
            bool wasInverted = active.IndexOf("_invert", StringComparison.OrdinalIgnoreCase) >= 0;
            int startIndex = 4 + active.IndexOf("ion/", StringComparison.OrdinalIgnoreCase);
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
        private void SetTextVisibility()
        {
            foreach (FrameworkElement element in responseFields.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel panel = (StackPanel)element;
                    foreach (TextBox textBox in panel.Children)
                    {
                        if (textBox.Uid == "res")
                        {
                            if (Utils.IsStringEmpty(textBox.Text))
                            {
                                CollapseAll(panel);
                            }
                            else
                            {
                                ShowAll(panel);
                            }
                        }
                    }
                }
            }
        }
        private void CollapseAll(StackPanel panel)
        {
            foreach (TextBox box in panel.Children)
            {
                box.Visibility = Visibility.Collapsed;
            }
        }
        private void ShowAll(StackPanel panel)
        {
            foreach (TextBox box in panel.Children)
            {
                box.Visibility = Visibility.Visible;
            }
        }
    }
}