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
    /// Interaction logic for autoPage.xaml
    /// </summary>
    /// 



    public partial class autoPage : UserControl
    {

        public string stationNumber = null;
        public int languageSelection = 1;
        public autoPage(int langSel)
        {
            languageSelection = langSel;
            InitializeComponent();
            Update();
            invertMap.Visibility = Visibility.Hidden;
          
        }
        private void UserControl_autoAndText_Loaded(object sender, RoutedEventArgs e)
        {
        }


        //-----------------Sjálfvirkar Spár-------------------------

        private void Update()
        {
            LandshlutarTableAdapter lta = new LandshlutarTableAdapter();
            StadirTableAdapter sta = new StadirTableAdapter();
            landshlutiCombo.ItemsSource = lta.GetData();
            stadurCombo.ItemsSource = sta.GetData();

        }


        private void stadurCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int errIndicatorAutoSpa; // 1 allt í lagi, 2 error

            if (stadurCombo.SelectedIndex == -1)
            {
                errIndicatorAutoSpa = 0;
            }
            else
            {
                autoClass aC = new autoClass();
                List<string> sjalfvirkarInfo = aC.sjalfvirkarXmlGet(stationNumber, languageSelection);
                if (sjalfvirkarInfo[6] == string.Empty)
                {
                    errIndicatorAutoSpa = 1;

                    stadurTextBox.Text = sjalfvirkarInfo[0];
                    timiTextBox.Text = sjalfvirkarInfo[1];
                    hitiTextBox.Text = sjalfvirkarInfo[2];
                    vindstTextBox.Text = sjalfvirkarInfo[3];
                    vindhrTextBox.Text = sjalfvirkarInfo[4];
                    vedurlysTextBox.Text = sjalfvirkarInfo[5];

                    #region(Visibility -> Visible)
                    string vedurlysing = sjalfvirkarInfo[5];

                    stadurTextBox.Visibility = Visibility.Visible;
                    timiTextBox.Visibility = Visibility.Visible;
                    hitiTextBox.Visibility = Visibility.Visible;
                    vindstTextBox.Visibility = Visibility.Visible;
                    vindhrTextBox.Visibility = Visibility.Visible;
                    if (vedurlysing.Trim() == string.Empty)
                    {
                        vedurlysTextBox.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        vedurlysTextBox.Visibility = Visibility.Visible;
                    }
                    #endregion
                }
                #region(ifError -> this)
                else
                {
                    errIndicatorAutoSpa = 2;

                    hitiTextBox.Text = string.Empty;
                    vindstTextBox.Text = string.Empty;
                    vindhrTextBox.Text = string.Empty;
                    vedurlysTextBox.Text = string.Empty;
                    vindhrTextBox.Visibility = Visibility.Hidden;
                    vedurlysTextBox.Visibility = Visibility.Hidden;

                    stadurTextBox.Text = sjalfvirkarInfo[0];
                    timiTextBox.Text = sjalfvirkarInfo[1];

                    hitiTextBox.Text = "Villa: Engar upplýsingar í augnablikinu";
                    vindstTextBox.Text = "Reyndu aftur eftir klukkutíma";

                    stadurTextBox.Visibility = Visibility.Visible;
                    timiTextBox.Visibility = Visibility.Visible;
                    hitiTextBox.Visibility = Visibility.Visible;
                    vindstTextBox.Visibility = Visibility.Visible;
                }
            }
            #endregion

           
            if (landshlutiCombo.SelectedIndex == 0)
            {

            }

            else if (landshlutiCombo.SelectedIndex == 1)
            {

            }

            else if (landshlutiCombo.SelectedIndex == 2)
            {

            }
            else if (landshlutiCombo.SelectedIndex == 3)
            {

            }
            else if (landshlutiCombo.SelectedIndex == 4)
            {

            }
            else if (landshlutiCombo.SelectedIndex == 5)
            {

            }
            else if (landshlutiCombo.SelectedIndex == 6)
            {

            }


            #region  --------------------------------------------------------------------------Sudausturland stations on map
            else if (landshlutiCombo.SelectedIndex == 7)
            {

                allarFrame.SetResourceReference(Image.SourceProperty, "sa_allar");

                if (stadurCombo.SelectedIndex == 0)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "fagurholsm");
                }
                else if (stadurCombo.SelectedIndex == 1)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "gigjukvisl");
                }
                else if (stadurCombo.SelectedIndex == 2)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "hofnihorna");
                }
                else if (stadurCombo.SelectedIndex == 3)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "ingolfshofdi");
                }
                else if (stadurCombo.SelectedIndex == 4)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "kirkjuklaust");
                }
                else if (stadurCombo.SelectedIndex == 5)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "kvisker");
                }
                else if (stadurCombo.SelectedIndex == 6)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "kvisker_veg");
                }
                else if (stadurCombo.SelectedIndex == 7)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "lomagnupur");
                }
                else if (stadurCombo.SelectedIndex == 8)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "myrdalssandur");
                }
                else if (stadurCombo.SelectedIndex == 9)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "oraefi");
                }
                else if (stadurCombo.SelectedIndex == 10)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "reynisfjall");
                }
                else if (stadurCombo.SelectedIndex == 11)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "skaftafell");
                }
                else if (stadurCombo.SelectedIndex == 12)
                {
                    stodvaframe.SetResourceReference(Image.SourceProperty, "skardsfjviti");
                }
            }
            #endregion


            else if (landshlutiCombo.SelectedIndex == 8)
            {

            }
            else if (landshlutiCombo.SelectedIndex == 9)
            {

            }
        }


        private void hiddenLandshl_TextChanged(object sender, TextChangedEventArgs e)
        {
            StadirTableAdapter sta = new StadirTableAdapter();

            int landshlutiId = Convert.ToInt32(hiddenLandshl.Text);

            var rows = from row in sta.GetData()
                       where row.Spásvæði.Equals(landshlutiId)
                       select row;

            VedurmonitorDataSet.StadirDataTable vmSDT = new VedurmonitorDataSet.StadirDataTable();
            rows.CopyToDataTable(vmSDT, LoadOption.OverwriteChanges);
            stadurCombo.ItemsSource = vmSDT;
        }

        private void hiddenStadur_TextChanged(object sender, TextChangedEventArgs e)
        {
            stationNumber = hiddenStadur.Text;
        }


        #region  ------------------------------------------------------------------------------------Kort fyrir autoPage normal + invert

        private void landshlutiCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            invertMap.Visibility = Visibility.Visible;

            string invCheck = "";
            if (invertMap.IsChecked == true)
            {
                invCheck = "_inv";
            }

            allarFrame.SetResourceReference(Image.SourceProperty, "empty");
            stodvaframe.SetResourceReference(Image.SourceProperty, "empty");


            if (landshlutiCombo.SelectedIndex == 0)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "fa" + invCheck);

            }

            else if (landshlutiCombo.SelectedIndex == 1)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "br" + invCheck);

            }

            else if (landshlutiCombo.SelectedIndex == 2)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "vf" + invCheck);

            }
            else if (landshlutiCombo.SelectedIndex == 3)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "nv" + invCheck);

            }
            else if (landshlutiCombo.SelectedIndex == 4)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "na" + invCheck);

            }
            else if (landshlutiCombo.SelectedIndex == 5)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "al" + invCheck);

            }
            else if (landshlutiCombo.SelectedIndex == 6)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "af" + invCheck);

            }

            else if (landshlutiCombo.SelectedIndex == 7)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "sa" + invCheck);
            }

            else if (landshlutiCombo.SelectedIndex == 8)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "su" + invCheck);

            }
            else if (landshlutiCombo.SelectedIndex == 9)
            {
                kortamynd.SetResourceReference(Image.SourceProperty, "mh" + invCheck);
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
        }



        #endregion

        
    }
}