using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace VedurMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private const int min_spl_time = 2500;

        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen();

            splash.Show();

            Stopwatch timer = new Stopwatch();

            timer.Start();

            MainWindow main = new MainWindow();

            timer.Stop();

            int remaining_time = min_spl_time - (int)timer.ElapsedMilliseconds;

            if (remaining_time > 0) Thread.Sleep(remaining_time);

            splash.Close();
        }



    }
}
