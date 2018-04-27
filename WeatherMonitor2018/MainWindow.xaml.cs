using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using WeatherMonitor2018.Pages;
using WeatherMonitorClassLibrary;

namespace WeatherMonitor2018
{
    public partial class MainWindow : Window
    {
        public TimeSpan _seconds = new TimeSpan(0, 0, 1);
        public Rectangle _bounds { get; private set; }
        ScreenShot _screenshot = new ScreenShot();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartClock();
            rightFrame.Content = new ForecastPage();
            leftFrame.Content = new StationPage();
        }
        private void StartClock()
        {
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _timer.Interval = _seconds;
            _timer.Start();
        }
        //klukka
        private void TimerTick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm");
            klukka.Content = time;
        }
        
        private void About_Click(object sender, RoutedEventArgs e)
        {
            rightFrame.Content = new AboutPage();
            closeAbout.Visibility = Visibility.Visible;
        }
        private void Close_About_Click(object sender, RoutedEventArgs e)
        {
            rightFrame.Content = new ForecastPage();
            closeAbout.Visibility = Visibility.Hidden;
        }
        private void Reset_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void English_Checked(object sender, RoutedEventArgs e)
        {
            //_languageSelection = 2;
            //rightFrame.Content = new ForecastPage();
            //leftFrame.Content = new ObservationTabControl(_observationService);
        }
        private void English_Unchecked(object sender, RoutedEventArgs e)
        {
            //_languageSelection = 1;
            //rightFrame.Content = new ForecastPage(_languageSelection);
            //leftFrame.Content = new ObservationTabControl(_observationService);
        }
        private void ScreenShot_Click(object sender, RoutedEventArgs e)
        {
            string response = _screenshot.SaveScreenshot();
            MessageBox.Show(response);
        }
        private void Color_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SolidColorBrush magicBrush = (SolidColorBrush)Resources["magicBrush"];
            if ((sliR != null) && (sliG != null) && (sliB != null))
            {
                magicBrush.Color = Color.FromRgb((byte)sliR.Value, (byte)sliG.Value, (byte)sliB.Value);
            }
        }

        #region BackgroundGif
        private void MyGifMediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundGif.Position = _seconds;
            backgroundGif.Play();
        }
        private void GifPlayer()
        {
            var color = (Color)ColorConverter.ConvertFromString("Black");
            SolidColorBrush brush = new SolidColorBrush(color);
            Settings.Foreground = brush;
            File.Foreground = brush;
            About.Foreground = brush;
        }
        private void ChangeBackground(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            backgroundGif.Source = null;
            if (menuItem.Name == "off")
            {
                colorSliders.Visibility = Visibility.Visible;
            }
            else
            {
                colorSliders.Visibility = Visibility.Collapsed;
                var uri = ( GetBackgroundPath() + menuItem.Name + ".gif");
                backgroundGif.Source = new Uri(uri);
            }
        }
        private string GetBackgroundPath()
        {
            var outPutDirectory = System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory());


            var path = outPutDirectory + "\\Assets\\Background\\";
            Console.WriteLine(path);
            return path;
        }
        #endregion BackgroundGif

    }
}

