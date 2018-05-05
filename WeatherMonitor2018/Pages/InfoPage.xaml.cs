using System;
using System.Runtime.Caching;
using System.Windows.Controls;
using System.Windows.Data;
using WeatherMonitor2018.Config;
using WeatherMonitorClassLibrary.Models;
using WeatherMonitorClassLibrary.XmlService;

namespace WeatherMonitor2018.Pages
{
    public partial class InfoPage : Page
    {
        static ObjectCache _applicationCache = MemoryCache.Default;
        private string aboutInfo = "";
        public InfoPage()
        {
            InitializeComponent();
            StationCacheInfoTextBlock.Text = Constants.AboutHttpAndCache;
            SetInfo();
            StartUpDateFromCache();
        }
        private void StartUpDateFromCache()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateTextFromCacheInterval);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
            dispatcherTimer.Start();
        }
        private void SetInfo()
        {
            var cacheInfo = _applicationCache.Get("StationsCacheCount") as InfoCacheModel;
            var httpInfo = _applicationCache.Get("StationsHttpCount") as InfoCacheModel;
            StationCacheCountTextBlock.Text = cacheInfo.Description + cacheInfo.Value;
            StationHttpCountTextBlock.Text = httpInfo.Description + httpInfo.Value;
        }

        private void UpdateTextFromCacheInterval(object sender, EventArgs e)
        {
            SetInfo();
        }

    }
}
