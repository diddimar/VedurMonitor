using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading;
using System.Windows;
using WeatherMonitor2018.Windows;
using WeatherMonitorClassLibrary.Models;

namespace WeatherMonitor2018
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int min_spl_time = 1000;
        protected override void OnStartup(StartupEventArgs e)
        {
            //string basePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            //ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            //configMap.ExeConfigFilename = "App.config";
            //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            InitStationCache();
            Stopwatch timer = new Stopwatch();
            StartScreen splash = new StartScreen();
            splash.Show();
            timer.Start();
            int remaining_time = min_spl_time - (int)timer.ElapsedMilliseconds;
            if (remaining_time > 0) Thread.Sleep(remaining_time);
            MainWindow mainWindow = new MainWindow();
            splash.Close();
            timer.Stop(); timer = null;
        }
         private void InitStationCache()
        {
            Station defaultStation = new Station { Time = "Nothing", Hiti = "0", Vedurlysing = "Hellað" };
            List<Station> list = new List<Station>();
            list.Add(defaultStation);
            list.Add(defaultStation);
            IEnumerable<Station> en = list;
            ObjectCache stationCache = MemoryCache.Default;
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(75) };
            stationCache.Add("stations", en, policy);
        }
    }
}
