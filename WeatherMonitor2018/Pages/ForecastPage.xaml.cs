using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Data;
using WeatherMonitor2018.Data.WeatherMonitorDataSetTableAdapters;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.ImageService;
using WeatherMonitorClassLibrary.Models;
using static WeatherMonitor2018.Data.WeatherMonitorDataSet;

namespace WeatherMonitor2018.Pages
{
    /// <summary>
    /// Interaction logic for rightPage1.xaml
    /// </summary>
    public partial class ForecastPage : UserControl
    {
        private ForecastService _forecastService;
        private ForecastImageResolver _forecastImageResolver;
        txtStadirDataTable _txtStadirDataTable;
        public ForecastPage(int langSel)
        {
            InitializeComponent();
            txtStadirTableAdapter tsta = new txtStadirTableAdapter();
            _txtStadirDataTable = tsta.GetData();
            tsta.Dispose();
            _forecastService = new ForecastService();
            _forecastImageResolver = new ForecastImageResolver();
        }
        private void ForecastPage_Loaded(object sender, RoutedEventArgs e)
        {
            //flokkurComboBox.SelectedIndex = 0;
            SetTextComboBox(1);
        }

        //public string basicTextPath = "http://xmlweather.vedur.is/?op_w=xml&type=txt&lang=is&view=xml&ids=";

        private void getTextButton_Click(object sender, RoutedEventArgs e)
        {


            //string completeTextPath = basicTextPath + textPathNumber;
            //XmlDocument xmlTextaspa = new XmlDocument();
            //xmlTextaspa.Load(completeTextPath);

            //XmlNodeList description = xmlTextaspa.GetElementsByTagName("content"),
            //                title = xmlTextaspa.GetElementsByTagName("title"),
            //                descriptionCreation = xmlTextaspa.GetElementsByTagName("creation"),
            //                descriptionValidFrom = xmlTextaspa.GetElementsByTagName("valid_from"),
            //                descriptionValidTo = xmlTextaspa.GetElementsByTagName("valid_to");

            //textDescriptionBox.Text = (description[0].InnerXml);

            //var Createdtime = DateTime.Parse(descriptionCreation[0].InnerXml);
            //var timeNow = DateTime.Now;
            //var differenceInTime = (Createdtime - timeNow).TotalHours;
            //string diff = differenceInTime.ToString().Substring(0, 5);
            //descriptionInfoBox.Text = "Birt fyrir " + diff + " klst";

        }

        private void flokkurComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void SetTextComboBox(int catId)
        {
            var rows = from row in _txtStadirDataTable
                       where row.CatId.Equals(catId)
                       select row;
            textspaDropdown.ItemsSource = rows;
            textspaDropdown.SelectedIndex = 0;
        }

        private void GetNewForecast(string stodvaNr)
        {
            Forecast textaInfo = _forecastService.GetForecast(stodvaNr);
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
            kortalayer.SetResourceReference(Image.SourceProperty, _forecastImageResolver.GetForecastMap(stodvaNr));
        }

        private void textComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string stationId = (e.AddedItems[0] as txtStadirRow).Field<int>("CatId").ToString();
            int stodvaNr = (e.AddedItems[0] as txtStadirRow).Field<int>("StodvaNr");

            GetNewForecast(stodvaNr.ToString());
            SetNewMapimage(stodvaNr);
        }

    }
}
