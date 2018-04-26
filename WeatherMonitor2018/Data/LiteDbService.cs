using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using WeatherMonitorClassLibrary.Models.DbObjects;

namespace WeatherMonitor2018.Data
{
    public static class LiteDbService
    {
        public static String DbPath()
        {
            String strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(strPath, "data\\database.sqlite");
            return path;
        }

        public static List<Region> GetRegions()
        {
            List<Region> regionList = new List<Region>();
            return regionList;
        }

        public static List<StationInfo> GetStations()
        {
            List<StationInfo> stationList = new List<StationInfo>();
            return stationList;
        }

        public static List<ForecastInfo> GetForecasts()
        {
            List<ForecastInfo> forecastList = new List<ForecastInfo>();
            return forecastList;
        }


    
    }
}
