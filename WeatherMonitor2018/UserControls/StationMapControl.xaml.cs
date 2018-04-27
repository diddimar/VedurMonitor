using System;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.ImageService;

namespace WeatherMonitor2018.UserControls
{
    public partial class StationMapControl : UserControl
    {
        public StationMapControl()
        {
            InitializeComponent();
        }
        public void ChangeImages(int regionDropdownIndex, int stationDropdownIndex)
        {
            allIndicatorsLayer.SetResourceReference(Image.SourceProperty, "empty");
            singleIndicatorLayer.SetResourceReference(Image.SourceProperty, "empty");
            string inverted = invertCheckbox.IsChecked == true ? "_inv" : String.Empty;
            string[] newMapLayers = StationImageResolver.GetMap(regionDropdownIndex);
            mapRootLayer.SetResourceReference(Image.SourceProperty, newMapLayers[0] + inverted);
            CheckIndicatorLayers(newMapLayers, regionDropdownIndex, stationDropdownIndex);
        }

        private void CheckIndicatorLayers(string[] newMapLayers, int regionDropdownIndex, int stationDropdownIndex)
        {
            if (Utils.ArrayInBounds(1, newMapLayers))
            {
                allIndicatorsLayer.SetResourceReference(Image.SourceProperty, newMapLayers[1]);
                CheckSationIndicator(regionDropdownIndex, stationDropdownIndex);
            };
        }
        public void CheckSationIndicator(int regionDropdownIndex, int stationDropdownIndex)
        {
            string indicator = StationImageResolver.SetIndicator(regionDropdownIndex, stationDropdownIndex);
            if (!string.IsNullOrEmpty(indicator))
            {
                singleIndicatorLayer.SetResourceReference(Image.SourceProperty, indicator);
            }
        }


        private void InvertCheckbox_Click(object sender, RoutedEventArgs e)
        {
            string active = mapRootLayer.GetValue(Image.SourceProperty).ToString();
            bool wasInverted = active.IndexOf("_invert", StringComparison.OrdinalIgnoreCase) >= 0;
            int startIndex = 4 + active.IndexOf("ons/", StringComparison.OrdinalIgnoreCase);
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

    }
}
