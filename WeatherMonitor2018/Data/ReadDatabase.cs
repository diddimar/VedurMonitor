using System;
using System.Collections.Generic;
using System.IO;
using SQLitePCL;
using WeatherMonitor2018.Data.Models;
using WeatherMonitorClassLibrary;

namespace WeatherMonitor2018.Data
{
    public static class ReadDatabase
    {

        #region Database Connections

        private static ISQLiteStatement GetDbRows(string tableName)
        {
            String strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(strPath, "data\\database.sqlite");
            SQLiteConnection dbConnection = new SQLiteConnection(path);
            string sSQL = $"SELECT * FROM {tableName}";
            ISQLiteStatement rows = dbConnection.Prepare(sSQL);
            dbConnection.Dispose();
            return rows;
        }

        public static List<Region> GetRegions()
        {
            ISQLiteStatement rows = GetDbRows("Region");
            List<Region> regionList = new List<Region>();
            while (rows.Step() == SQLiteResult.ROW)
            {
                regionList.Add(ParseRegion(rows));
            }
            return regionList;
        }

        public static List<StationInfo> GetStations()
        {
            ISQLiteStatement rows = GetDbRows("Station");
            List<StationInfo> stationList = new List<StationInfo>();
            while (rows.Step() == SQLiteResult.ROW)
            {
                stationList.Add(ParseStation(rows));
            }
            return stationList;
        }

        public static List<ForecastInfo> GetForecasts()
        {
            ISQLiteStatement rows = GetDbRows("Forecast");
            List<ForecastInfo> forecastList = new List<ForecastInfo>();
            while (rows.Step() == SQLiteResult.ROW)
            {
                forecastList.Add(ParseForecast(rows));
            }
            return forecastList;
        }

        #endregion


        #region Parsing 

        private static Region ParseRegion(ISQLiteStatement row)
        {
            var region = new Region();
            for (int x = 0; x < row.ColumnCount; x++)
            {
                var columnLabel = row.ColumnName(x);
                if (typeof(Region).HasProperty(columnLabel))
                {
                    SQLiteType dataType = row.DataType(x);
                    if (dataType == SQLiteType.INTEGER)
                        region.Id = int.Parse(row[x].ToString());
                    else
                        region.Name = row[x] as string;
                }
            }
            return region;
        }

        private static StationInfo ParseStation(ISQLiteStatement row)
        {
            StationInfo station = new StationInfo();
            for (int x = 0; x < row.ColumnCount; x++)
            {
                string columnName = row.ColumnName(x); var i = row[x];
                if (typeof(StationInfo).HasProperty(columnName))
                {
                    station = SwitchStationColumnName(station, columnName, row[x].ToString());
                }
            }
            return station;
        }
        private static StationInfo SwitchStationColumnName(StationInfo station, string columnName, string value)
        {
            switch (columnName)
            {
                default:
                    station.Id = int.Parse(value);
                    return station;
                case "Name":
                    station.Name = value;
                    return station;
                case "Type":
                    station.Type = value;
                    return station;
                case "WMO_Number":
                    station.WMO_Number = int.Parse(value);
                    return station;
                case "Shortname":
                    station.Shortname = value;
                    return station;
                case "Region":
                    station.Region = int.Parse(value);
                    return station;
                case "Location":
                    station.Location = value;
                    return station;
                case "Altitude":
                    station.Altitude = float.Parse(value);
                    return station;
                case "UpphafAthuguna":
                    station.UpphafAthuguna = int.Parse(value);
                    return station;
                case "Owner":
                    station.Owner = value;
                    return station;
                case "StationNumber":
                    station.StationNumber = int.Parse(value);
                    return station;
            }
        }

        private static ForecastInfo ParseForecast(ISQLiteStatement row)
        {
            var forecast = new ForecastInfo();
            for (int x = 0; x < row.ColumnCount; x++)
            {
                var columnLabel = row.ColumnName(x);
                if (typeof(ForecastInfo).HasProperty(columnLabel))
                {
                    SQLiteType dataType = row.DataType(x);
                    if (columnLabel == "Name")
                        forecast.Name = row[x] as string;
                    else if(columnLabel == "Id")
                        forecast.Id = int.Parse(row[x].ToString());
                    else if(columnLabel == "CategoryId")
                        forecast.CategoryId = int.Parse(row[x].ToString());
                    else if (columnLabel == "ForecastNumber")
                        forecast.ForecastNumber = int.Parse(row[x].ToString());
                }
            }
            return forecast;
        }

        #endregion


    }
}
