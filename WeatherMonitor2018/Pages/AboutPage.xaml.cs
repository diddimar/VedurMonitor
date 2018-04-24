using System.Windows.Controls;
using WeatherMonitorClassLibrary;

namespace WeatherMonitor2018.Pages
{
    public partial class AboutPage : UserControl
    {
        ObservationService _observationService;
        public AboutPage(ObservationService os)
        {
            InitializeComponent();
            _observationService = os;
        }

        private void AboutPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            cachedTextBox.Text = "Requests from cache: " + _observationService.cacheCount.ToString();
            requestTextBox.Text = "Requests from http: " + _observationService.httpCount.ToString();
        }
    }
}
