using System;
using System.Windows;
using System.Windows.Controls;

namespace VedurMonitor
{
    /// <summary>
    /// LeftPage   - lódar AutoPage inní fimm tabs
    /// </summary>
    public partial class LeftPage : UserControl
    {
        private void UserControlLeftPageLoaded(object sender, RoutedEventArgs e)
        {
        }
        public int languageSelection = 1;
        public LeftPage(int langSel)
        {
            languageSelection = langSel;
            try
            {
                InitializeComponent();
                spa1.Content = new LeftPageSection(languageSelection);
                spa2.Content = new LeftPageSection(languageSelection);
                spa3.Content = new LeftPageSection(languageSelection);
                spa4.Content = new LeftPageSection(languageSelection);
                spa5.Content = new LeftPageSection(languageSelection);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
