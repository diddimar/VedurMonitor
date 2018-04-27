using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data;
using WeatherMonitor2018.Data.Models;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.ImageService;
using WeatherMonitorClassLibrary.Models.XmlResponses;
using WeatherMonitorClassLibrary.XmlService;

namespace WeatherMonitor2018.Pages
{
    public partial class StationTab : UserControl
    {
        private StationInfo _selectedStation;
        private IEnumerable<Region> _regionList;
        private IEnumerable<StationInfo> _stationList;
        public static readonly RoutedEvent LandshlutiChangedEvent = EventManager.RegisterRoutedEvent(
            "LandshlutiChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StationTab));

        public StationTab(IEnumerable<Region> regionList, IEnumerable<StationInfo> stationList, int selectedRegion)
        {
            InitializeComponent();
            _regionList = regionList;
            _stationList = stationList;
            regionDropdown.ItemsSource = _regionList;
            regionDropdown.SelectedIndex = selectedRegion;
        }
        public event RoutedEventHandler LandshlutiChanged
        {
            add { AddHandler(LandshlutiChangedEvent, value); }
            remove { RemoveHandler(LandshlutiChangedEvent, value); }
        }
        private void RegionDropDownChange(object sender, SelectionChangedEventArgs e)
        {
            int regionId = (e.AddedItems[0] as Region).Id;
            UpdateStationDropdown(regionId);
            RaiseUpdateTabHeaderEvent(regionId);
            ChangeImages();
        }
        private void RaiseUpdateTabHeaderEvent(int regionId)
        {
            string name = _regionList.Where(x => x.Id == regionId).Select(x => x.Name).First();
            RaiseEvent(new RoutedEventArgs(LandshlutiChangedEvent, name)); //Bubble Event to ObservationPageWrapper
        }
        private void StationSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) { return; }
            _selectedStation = (e.AddedItems[0] as StationInfo);
            stationIdText.Text = "Stöðvanúmer: " + _selectedStation.StationNumber.ToString();
            LoadStation();
        }
        private void LoadStation()
        {
            Station response = StationService.Get(_selectedStation.StationNumber.ToString());
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
        private void SetStationDbInfo(StationInfo selectedStation)
        {
            altitudeTextBox.Text = Convert.ToInt32(selectedStation.Altitude).ToString() + " metrar";
            ownerTextBox.Text = "Eigandi: " + selectedStation.Owner;
        }
        private void UpdateStationDropdown(int regionId)
        {
            var rows = _stationList.Where(x => x.Region == regionId);
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
            string[] newMapLayers = StationImageResolver.GetMap(regionDropdown.SelectedIndex);
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
            string indicator = StationImageResolver.SetIndicator(regionDropdown.SelectedIndex, stationDropdown.SelectedIndex);
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