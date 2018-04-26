using System.Windows;
using System.Windows.Controls;
using WeatherMonitorClassLibrary;

namespace WeatherMonitor2018.Pages
{
    public partial class StationPage : UserControl
    {
        public StationPage()
        {
            AddHandler(StationTab.LandshlutiChangedEvent, new RoutedEventHandler(LandshlutiChangedEventHandler));
            InitializeComponent();
            LoadTabs();
        }

        public void LoadTabs()
        {
            tab1.Content = new StationTab(0);
            tab2.Content = new StationTab(1);
            tab3.Content = new StationTab(2);
            tab4.Content = new StationTab(8);
            tab5.Content = new StationTab(7);
        }

        private void LandshlutiChangedEventHandler(object sender, RoutedEventArgs e)
        {
            string shortname = Utils.Truncate(e.OriginalSource.ToString(), 13);
            UpdateTabHeader(shortname);
        }

        private void UpdateTabHeader(string landshluti)
        {
            switch (observationTabControl.SelectedIndex)
            {
                case 0:
                    tab1.Header = landshluti;
                    break;
                case 1:
                    tab2.Header = landshluti;
                    break;
                case 2:
                    tab3.Header = landshluti;
                    break;
                case 3:
                    tab4.Header = landshluti;
                    break;
                case 4:
                    tab5.Header = landshluti;
                    break;
            }
        }

    }
}