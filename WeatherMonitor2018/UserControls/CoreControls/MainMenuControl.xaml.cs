using System;
using System.Windows;
using System.Windows.Controls;

namespace WeatherMonitor2018.UserControls.CoreControls
{
    public partial class MainMenuControl : UserControl
    {
        //public Rectangle _bounds { get; private set; }
        //ScreenShot _screenshot = new ScreenShot();
        public MainMenuControl()
        {
            InitializeComponent();
        }
        //private void Close_About_Click(object sender, RoutedEventArgs e)
        //{
        //    //rightFrame.Content = new ForecastPage();
        //    //closeAbout.Visibility = Visibility.Hidden;
        //}
        //private void About_Click(object sender, RoutedEventArgs e)
        //{
        //    //MainWindow.rightFrame.Content = new AboutPage();
        //    //MainWindow.closeAbout.Visibility = Visibility.Visible;
        //}
        private void Reset_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // BackgroundSelect
        public static readonly RoutedEvent BackgroundSelectEvent = EventManager.RegisterRoutedEvent(
          "BackgroundSelectEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainMenuControl));
        public event RoutedEventHandler BackgroundSelectEventHandler
        {
            add { AddHandler(BackgroundSelectEvent, value); }
            remove { RemoveHandler(BackgroundSelectEvent, value); }
        }
        private void Background_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            RaiseEvent(new RoutedEventArgs(BackgroundSelectEvent, menuItem.Name));
        }
        // End Background select
    }
    //private void ScreenShot_Click(object sender, RoutedEventArgs e)
    //{
    //    string response = _screenshot.SaveScreenshot();
    //    MessageBox.Show(response);
    //}
    //private void Color_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    //{
    //    SolidColorBrush magicBrush = (SolidColorBrush)Resources["magicBrush"];
    //    if ((sliR != null) && (sliG != null) && (sliB != null))
    //    {
    //        magicBrush.Color = Color.FromRgb((byte)sliR.Value, (byte)sliG.Value, (byte)sliB.Value);
    //    }
    //}

    //private void ChangeBackground(object sender, RoutedEventArgs e)
    //{
    //    // Not setup
    //}
}

