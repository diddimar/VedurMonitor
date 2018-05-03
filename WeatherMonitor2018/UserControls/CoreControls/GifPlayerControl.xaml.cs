using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WeatherMonitor2018.Config;
using WpfAnimatedGif;

namespace WeatherMonitor2018.UserControls.CoreControls
{
    public partial class GifPlayerControl : UserControl
    {
        public GifPlayerControl()
        {
            AddHandler(MainMenuControl.BackgroundSelectEvent, new RoutedEventHandler(BackgroundSelectEventHandler));
            InitializeComponent();
        }
        private void BackgroundSelectEventHandler(object sender, RoutedEventArgs e)
        {
            ChangeBackground(e.OriginalSource.ToString());
        }
        private void ChangeBackground(string name)
        {
            if (name == "off")
            {
                ImageBehavior.SetAnimatedSource(Background, null);
            }
            else
            {
                var uri = (GetBackgroundPath() + name + ".gif");
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(uri);
                image.EndInit();
                ImageBehavior.SetAnimatedSource(Background, image);
            }
        }
        private string GetBackgroundPath()
        {
            var outPutDirectory = System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var path = Constants.AssetsRoot + "Background/";
            Console.WriteLine(path);
            return path;
        }

    }
}
