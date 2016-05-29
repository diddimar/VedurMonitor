using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;
using VedurClassLibrary;
using vedurMonitor.VedurmonitorDataSetTableAdapters;

namespace vedurMonitor
{
    /// <summary>
    /// Interaction logic for textaSparPage.xaml
    /// </summary>
    public partial class textPage : UserControl
    {
        public string stationNumber = null;
        public textPage()
        {
            InitializeComponent();
        }
        private void UserControl_text_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #region --------------  Textaspár  -----------------------------------
        public string basicTextPath = "http://xmlweather.vedur.is/?op_w=xml&type=txt&lang=is&view=xml&ids=";
        public string textPathNumber;

        private void getTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (textComboBox.SelectedIndex == 0)
            {
                textPathNumber = "2";
            }
            else if (textComboBox.SelectedIndex == 1)
            {
                textPathNumber = "3";
            }
            else if (textComboBox.SelectedIndex == 2)
            {
                textPathNumber = "31";
            }
            else if (textComboBox.SelectedIndex == 3)
            {
                textPathNumber = "32";
            }
            else if (textComboBox.SelectedIndex == 4)
            {
                textPathNumber = "33";
            }
            else if (textComboBox.SelectedIndex == 5)
            {
                textPathNumber = "34";
            }
            else if (textComboBox.SelectedIndex == 6)
            {
                textPathNumber = "35";
            }
            else if (textComboBox.SelectedIndex == 7)
            {
                textPathNumber = "36";
            }
            else if (textComboBox.SelectedIndex == 8)
            {
                textPathNumber = "37";
            }
            else if (textComboBox.SelectedIndex == 9)
            {
                textPathNumber = "38";
            }
            else if (textComboBox.SelectedIndex == 10)
            {
                textPathNumber = "39";
            }
            else if (textComboBox.SelectedIndex == 11)
            {
                textPathNumber = "30";
            }

            string completeTextPath = basicTextPath + textPathNumber;
            XmlDocument xmlTextaspa = new XmlDocument();
            xmlTextaspa.Load(completeTextPath);

            XmlNodeList description = xmlTextaspa.GetElementsByTagName("content"),
                            title = xmlTextaspa.GetElementsByTagName("title"),
                            descriptionCreation = xmlTextaspa.GetElementsByTagName("creation"),
                            descriptionValidFrom = xmlTextaspa.GetElementsByTagName("valid_from"),
                            descriptionValidTo = xmlTextaspa.GetElementsByTagName("valid_to");

            textDescriptionBox.Text = (description[0].InnerXml);

            var Createdtime = DateTime.Parse(descriptionCreation[0].InnerXml);
            var timeNow = DateTime.Now;
            var differenceInTime = (Createdtime - timeNow).TotalHours;
            string diff = differenceInTime.ToString().Substring(0, 5);
            descriptionInfoBox.Text = "Birt fyrir " + diff + " klst";

        }





        #endregion


        //--------------------------------------------------------------------------------------------------



    }
}
