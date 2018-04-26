using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using WeatherMonitorClassLibrary.Models.DbObjects;

namespace WeatherMonitor2018.Data
{
    public static class SQLiteService
    {
        public static String DbPath()
        {
            String strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(strPath, "data\\database.sqlite");
            return path;
        }

        public static List<Region> GetRegions()
        {
            string stm = "Select * from Region";
            List<Region> regionList = new List<Region>();
            using (SQLiteConnection conn = new SQLiteConnection($@"Data Source={DbPath()};Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var i = new Region();
                            i.Id = int.Parse(rdr["Id"].ToString());
                            i.Name = rdr["Name"].ToString();
                            regionList.Add(i);
                        }
                    }
                }
                conn.Close();
            }
            return regionList;
        }

        public static List<StationInfo> GetStations()
        {
            string stm = "Select * from Station";
            List<StationInfo> stationList = new List<StationInfo>();
            using (SQLiteConnection conn = new SQLiteConnection($@"Data Source={DbPath()};Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var i = new StationInfo();
                            i.Id = int.Parse(rdr["Id"].ToString());
                            i.Name = rdr["Name"].ToString();
                            i.Type = rdr["Type"].ToString();
                            i.WMO_Number = int.Parse(rdr["WMO_Number"].ToString());
                            i.Shortname = rdr["Shortname"].ToString();
                            i.Region = int.Parse(rdr["Region"].ToString());
                            i.Location = rdr["Location"].ToString();
                            i.UpphafAthuguna = int.Parse(rdr["UpphafAthuguna"].ToString());
                            i.Altitude = float.Parse(rdr["Altitude"].ToString(), CultureInfo.InvariantCulture);
                            i.Owner = rdr["Owner"].ToString();
                            i.StationNumber = int.Parse(rdr["StationNumber"].ToString());
                            stationList.Add(i);
                        }
                    }
                }
                conn.Close();
            }
            return stationList;
        }

        public static List<ForecastInfo> GetForecasts()
        {
            string stm = "Select * from Forecast";
            List<ForecastInfo> forecastList = new List<ForecastInfo>();
            using (SQLiteConnection conn = new SQLiteConnection($@"Data Source={DbPath()};Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var i = new ForecastInfo();
                            i.Id = int.Parse(rdr["Id"].ToString());
                            i.Name = rdr["Name"].ToString();
                            i.CategoryId = int.Parse(rdr["CategoryId"].ToString());
                            i.ForecastNumber = int.Parse(rdr["ForecastNumber"].ToString());
                            forecastList.Add(i);
                        }
                    }
                }
                conn.Close();
            }
            return forecastList;
        }


    
    }
}
