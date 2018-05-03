using System.Windows;

namespace WeatherMonitor2018.DialogWindows
{
    public partial class NoConnectionDialog : Window
    {
        public NoConnectionDialog()
        {
            InitializeComponent();
        }

        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
