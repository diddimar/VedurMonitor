using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml;
using VedurClassLibrary;
using VedurMonitor.VedurmonitorDataSetTableAdapters;

namespace VedurMonitor
{
    /// <summary>
    /// Interaction logic for rightPage1.xaml
    /// </summary>
    public partial class rightPage1 : UserControl
    {
        public int txtCat = 0;
        public string textStationNumber = null;
        public int languageSelection = 1;

        public rightPage1(int langSel)
        {
            InitializeComponent();
            languageSelection = langSel;
            textDescriptionBox.Text = "Veljið Flokk";
            //txtStadirTableAdapter tsta = new txtStadirTableAdapter();
            //textComboBox.ItemsSource = tsta.GetData();
        }
        private void UserControl_rightPage1_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #region --------------  Textaspár  -----------------------------------
        //public string basicTextPath = "http://xmlweather.vedur.is/?op_w=xml&type=txt&lang=is&view=xml&ids=";
        //public string textPathNumber;

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







        #endregion

        private void flokkurComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flokkurComboBox.SelectedIndex == 0)
            {
                txtCat = 1;
                textDescriptionBox.Text = "Veljið Textaspá";
                descriptionInfoBox.Text = "";
                
            }
            else if (flokkurComboBox.SelectedIndex == 1)
            {
                txtCat = 3;
            }
            else
            {
                txtCat = 2;
            }
            txtStadirTableAdapter tsta = new txtStadirTableAdapter();

            var rows = from row in tsta.GetData()
                       where row.CatId.Equals(txtCat)
                       select row;

            VedurmonitorDataSet.txtStadirDataTable vmTSDT = new VedurmonitorDataSet.txtStadirDataTable();
            rows.CopyToDataTable(vmTSDT, LoadOption.OverwriteChanges);
            textComboBox.ItemsSource = vmTSDT;


        }



        private void textComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (textComboBox.SelectedIndex == -1)
            {

            }
            else if (textComboBox.SelectedIndex == 1 )
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "phb");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);

                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 6)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pmh");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 7)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "psu");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 8)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pfa");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 9)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pbr");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 10)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pvf");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 11)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pnv");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 12)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pna");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 13)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "pal");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 14)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "paf");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else if (textComboBox.SelectedIndex == 15)
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "psa");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";
            }
            else
            {
                kortalayer.SetResourceReference(Image.SourceProperty, "allt");
                textClass tC = new textClass();
                List<string> textaInfo = tC.textiXmlGet(textStationNumber, languageSelection);
                textDescriptionBox.Text = textaInfo[0];
                DateTime createdTime = DateTime.Parse(textaInfo[1]);
                DateTime currentTime = DateTime.Now;
                double differenceInTime = (createdTime - currentTime).TotalHours;    //er einhver vitleysa að eiga sér stað hér ? Gæti verið regional vesen!
                string diff = differenceInTime.ToString().Substring(0, 5);
                descriptionInfoBox.Text = "Spá skrifuð fyrir " + diff + " klst. síðan.";


            }







        }

        private void textspaHidden_TextChanged(object sender, TextChangedEventArgs e)
        {
            textStationNumber = textIdHidden.Text;
        }
    }
}
