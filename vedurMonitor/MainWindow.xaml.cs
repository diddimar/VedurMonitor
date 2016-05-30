using System;
using System.Windows;
using System.Windows.Media;
using VedurClassLibrary;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls;

namespace VedurMonitor
{
    /// <summary>
    /// MainWindow = Gif bakgrunnur / klukka / MenuItem Suff / leftPage- & rightPage-frame 
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            rightFrame.Content = new rightPage1(languageSelection);
            leftFrame.Content = new LeftPage1(languageSelection);

            
        }
        

        //klukka
        private void TimerTick(object sender, EventArgs e)    
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            klukka.Content = time;
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Start();
        }

        //Menu Stuff
        private void sli_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            SolidColorBrush magicBrush = (SolidColorBrush)Resources["magicBrush"];
            if ((sliR != null) && (sliG != null) && (sliB != null))
            {
                magicBrush.Color = Color.FromRgb((byte)sliR.Value, (byte)sliG.Value, (byte)sliB.Value);
            }
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            rightFrame.Content = new aboutPage();
            closeAbout.Visibility = Visibility.Visible;
        }
        private void ok_click(object sender, RoutedEventArgs e)
        {
            rightFrame.Content = new rightPage1(languageSelection);
            closeAbout.Visibility = Visibility.Hidden;
        }
        private void quit_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Reset_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void screenShot_click(object sender, RoutedEventArgs e)
        {
            _screenshot.SS();

        }
        public static string kortasource = "";
        public int languageSelection = 1;
        public Rectangle Bounds { get; private set; }
        ScreenShot1 _screenshot = new ScreenShot1();
        private void engCheck_Checked(object sender, RoutedEventArgs e)
        {
            languageSelection = 2;
            rightFrame.Content = new rightPage1(languageSelection);
            leftFrame.Content = new LeftPage1(languageSelection);
        }
        private void engCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            languageSelection = 1;
            rightFrame.Content = new rightPage1(languageSelection);
            leftFrame.Content = new LeftPage1(languageSelection);
        }

        //Gif stuff
        private void myGifMediaEnded(object sender, RoutedEventArgs e)
        {
            myGifS.Position = new TimeSpan(0, 0, 1);
            myGifS.Play();
        }
        private void gifPlayer()
        {
            var color = (Color)ColorConverter.ConvertFromString("Black");
            SolidColorBrush brush = new SolidColorBrush(color);
            Settings.Foreground = brush;
            File.Foreground = brush;
            Presets.Foreground = brush;
            About.Foreground = brush;


        }

        private void RainClick(object sender, RoutedEventArgs e)
        {
            colorSliders.Visibility = Visibility.Collapsed;
            myGifS.Source = null;
            myGifS.Source = new Uri(@"\\MORDOR.ntv.local\NemendurData$\davidg.v16\Desktop\TheMonitor\vedurMonitor\ImageFolder\rain.gif");


        }
        private void Sunset_click(object sender, RoutedEventArgs e)
        {
            colorSliders.Visibility = Visibility.Collapsed;
            myGifS.Source = null;
            var uri = (@"\\MORDOR.ntv.local\NemendurData$\davidg.v16\Desktop\TheMonitor\vedurMonitor\ImageFolder\sunSky.gif");
            myGifS.Source =  new Uri(uri);
        }
        private void hurr_click(object sender, RoutedEventArgs e)
        {
            colorSliders.Visibility = Visibility.Collapsed;
            myGifS.Source = null;
            var uri = (@"\\MORDOR.ntv.local\NemendurData$\davidg.v16\Desktop\TheMonitor\vedurMonitor\ImageFolder\hurricane.gif");
            myGifS.Source = new Uri(uri);
        }
        private void snow_click(object sender, RoutedEventArgs e)
        {
            colorSliders.Visibility = Visibility.Collapsed;
            myGifS.Source = null;
            var uri = (@"\\MORDOR.ntv.local\NemendurData$\davidg.v16\Desktop\TheMonitor\vedurMonitor\ImageFolder\snowing.gif");
            myGifS.Source = new Uri(uri);
        }
        private void mec_click(object sender, RoutedEventArgs e)
        {
            colorSliders.Visibility = Visibility.Collapsed;
            myGifS.Source = null;
            var uri = (@"\\MORDOR.ntv.local\NemendurData$\davidg.v16\Desktop\TheMonitor\vedurMonitor\ImageFolder\mecanoOut.gif");
            myGifS.Source = new Uri(uri);
        }
        private void mec2_click(object sender, RoutedEventArgs e)
        {
            colorSliders.Visibility = Visibility.Collapsed;
            myGifS.Source = null;
            var uri = (@"\\MORDOR.ntv.local\NemendurData$\davidg.v16\Desktop\TheMonitor\vedurMonitor\ImageFolder\mecano.gif");
            myGifS.Source = new Uri(uri);
        }





        private void Off_click(object sender, RoutedEventArgs e)
        {
            myGifS.Source = null;

            colorSliders.Visibility = Visibility.Visible;
            
        }
        





    }
}
