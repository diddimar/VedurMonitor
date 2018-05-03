using System.Windows;
using System.Windows.Input;
using WeatherMonitor2018.DialogWindows;
using WeatherMonitor2018.Pages;
using WeatherMonitorClassLibrary;

namespace WeatherMonitor2018
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfirmInternetConnection();
        }

        private void OpenContent()
        {
            rightFrame.Content = new ForecastPage();
            leftFrame.Content = new StationPage();
        }

        private void ConfirmInternetConnection()
        {
            if (Utils.IsNetworkAvailable())
            {
                OpenContent();
            }
            else if (DialogResult() == true)
            {
                ConfirmInternetConnection();
            }
        }

        private new bool? DialogResult()
        {
            var dialog = new NoConnectionDialog();
            dialog.Owner = this;
            var res = dialog.ShowDialog();
            return res;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            // Window_LocationChanged();
        }

        // Not Used
        private void Window_LocationChanged()
        {
            foreach (Window win in this.OwnedWindows)
            {
                if(win.Name.Equals("NoConnection"))
                {
                    win.Top = this.Top + (this.Height / 2.6);
                    win.Left = this.Left + (this.Width / 2.9);
                }
            }
        }

    }
}

