using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using WeatherMonitor2018.Windows;

namespace WeatherMonitor2018
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int min_spl_time = 2500;
        protected override void OnStartup(StartupEventArgs e)
        {
            string basePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = "App.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            StartScreen splash = new StartScreen();
            splash.Show();
            Stopwatch timer = new Stopwatch();
            timer.Start();

            MainWindow main = new MainWindow();

            int remaining_time = min_spl_time - (int)timer.ElapsedMilliseconds;
            if (remaining_time > 0) Thread.Sleep(remaining_time);

            timer.Stop();
            splash.Close();
        }

    }
}
