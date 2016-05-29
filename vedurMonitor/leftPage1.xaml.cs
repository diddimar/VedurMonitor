using System;
using System.Windows;
using System.Windows.Controls;

namespace VedurMonitor
{
    /// <summary>
    /// LeftPage   - lódar AutoPage inní fimm tabs
    /// </summary>
    public partial class LeftPage1 : UserControl
    {
        private void UserControlLeftPage1Loaded(object sender, RoutedEventArgs e)
        {
        }
        public int languageSelection = 1;
        public LeftPage1(int langSel)
        {
            languageSelection = langSel;
            try
            {
                InitializeComponent();
                spa1.Content = new autoPage(languageSelection);
                spa2.Content = new autoPage(languageSelection);
                spa3.Content = new autoPage(languageSelection);
                spa4.Content = new autoPage(languageSelection);
                spa5.Content = new autoPage(languageSelection);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
