using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data;
using WeatherMonitorClassLibrary.ImageService;
using WeatherMonitorClassLibrary.Models.DbObjects;
using WeatherMonitorClassLibrary.Models.XmlResponses;
using WeatherMonitorClassLibrary.XmlService;

namespace WeatherMonitor2018.Pages
{
    public partial class ForecastPage : UserControl
    {
        public ForecastPage()
        {
            InitializeComponent();
        }

        private void ForecastPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetForecastComboBox(1);
        }

        private void SetForecastComboBox(int catId)
        {
            var rows = from row in SQLiteService.GetForecasts()
                       where row.CategoryId.Equals(catId)
                       select row;
            forecastDropdown.ItemsSource = rows;
            forecastDropdown.SelectedIndex = 0;
        }

        private void GetNewForecast(string stodvaNr)
        {
            Forecast textaInfo = ForecastService.GetForecast(stodvaNr);
            forecastTextBox.Text = textaInfo.Content;

            DateTime parsed;
            if (DateTime.TryParse(textaInfo.Creation, out parsed))
            {
                parsed = DateTime.Parse(textaInfo.Creation);
                DateTime now = DateTime.Now;
                TimeSpan span = now.Subtract(parsed);
                if(span.Hours > 0)
                    forecastInfoBox.Text = $"Spá skrifuð fyrir {span.Hours} klst";
                else
                    forecastInfoBox.Text = $"Spá skrifuð fyrir {span.Minutes} min";

            }else
            {
                forecastInfoBox.Text = $"Reynið aftur síðar";
            }
            var validTo = textaInfo.Valid_to;
            var validFrom = textaInfo.Valid_from;

        }
 
        private void SetNewMapimage(int stodvaNr)
        {
            kortalayer.SetResourceReference(Image.SourceProperty, ForecastImageResolver.GetMap(stodvaNr));
        }

        private void textComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int forecastNr = (e.AddedItems[0] as ForecastInfo).ForecastNumber;
            GetNewForecast(forecastNr.ToString());
            SetNewMapimage(forecastNr);
        }

    }
}
