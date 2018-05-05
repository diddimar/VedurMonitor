using System;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitor2018.Pages;

namespace WeatherMonitor2018.UserControls.CoreControls
{
    public partial class MainMenuControl : UserControl
    {
        public MainMenuControl()
        {
            InitializeComponent();
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
        public static readonly RoutedEvent InfoSelectEvent = EventManager.RegisterRoutedEvent(
          "InfoSelectEvent ", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainMenuControl));
        public event RoutedEventHandler InfoSelectEventHandler
        {
            add { AddHandler(InfoSelectEvent , value); }
            remove { RemoveHandler(InfoSelectEvent , value); }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(InfoSelectEvent));
        }
    }
}

