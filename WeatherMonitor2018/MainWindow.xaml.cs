using System;
using System.Windows;
using System.Windows.Input;
using WeatherMonitor2018.DialogWindows;
using WeatherMonitor2018.Pages;
using WeatherMonitor2018.UserControls.CoreControls;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.XmlService;

namespace WeatherMonitor2018
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AddHandler(MainMenuControl.InfoSelectEvent, new RoutedEventHandler(MainMenuControlInfoSelectEventHandler));
            AddHandler(MainMenuControl.BackgroundSelectEvent, new RoutedEventHandler(MainMenuControlBackgroundSelectEventHandler));
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StationService.UpdateCacheInfoCache(0);
            StationService.UpdateCacheInfoHttp(0);
            ConfirmInternetConnection();
        }

        private void OpenContent()
        {
            LeftFrame.Content = new StationPage();
            RightFrame.Content = new ForecastPage();
        }

        private void MainMenuControlInfoSelectEventHandler(object sender, RoutedEventArgs e)
        {
            RightFrame.Content = new InfoPage();
            RightFrameControl.Visibility = Visibility.Visible;
        }
        private void RightFrameControl_Click(object sender, RoutedEventArgs e)
        {
            RightFrameControl.Visibility = Visibility.Collapsed;
            RightFrame.Content = new ForecastPage();
        }

        private void ConfirmInternetConnection()
        {
            if (Utils.IsConnected())
            {
                OpenContent();
            }
            else if (DialogResult() == true)
            {
                ConfirmInternetConnection();
            }
        }

        public new bool? DialogResult()
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

        // Gif background
        public static RoutedEvent ChangeBackgroundGifEvent = EventManager.RegisterRoutedEvent("ChangeBackgroundGif", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(MainWindow));
        public event RoutedEventHandler PreviewCloseApplication
        {
            add { AddHandler(ChangeBackgroundGifEvent, value); }
            remove { RemoveHandler(ChangeBackgroundGifEvent, value); }
        }
        private void MainMenuControlBackgroundSelectEventHandler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ChangeBackgroundGifEvent, e.OriginalSource));
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

