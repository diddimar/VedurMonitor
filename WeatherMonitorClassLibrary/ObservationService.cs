using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Xml;
using System.Xml.Serialization;
using WeatherMonitorClassLibrary.Models;

namespace WeatherMonitorClassLibrary
{
    public class ObservationService
    {
        WindDirection _windDirection;
        ObjectCache _applicationCache;
        private int cacheCount = 0;
        private int httpCount = 0;
        public ObservationService()
        {
            _windDirection = new WindDirection();
            _applicationCache = MemoryCache.Default;
        }
        public Station GetStationObservation(string stationId)
        {
            var station = CheckIfStationIsInCache(stationId);
            if (station != null && CheckCachedStationAge(station))
            {
                cacheCount++;
                Console.WriteLine($"{cacheCount} stations received from cache.");
                return station;
            }
            else
            {
                httpCount++;
                Console.WriteLine($"{httpCount} http request for stations made.");
                return GetXML(stationId);
            }
        }

        private Station GetXML(string stationId)
        {
            string path = @"http://xmlweather.vedur.is/?op_w=xml&type=obs&lang=is&view=xml&ids=" + stationId;
            XmlDocument document = Utils.GetXmlDocument(path);
            Station response = DeserializeObservationDocument(document);

            if (response == null)
                return new Station { Valid = 0, Err = "Villa. Reynið aftur seinna..." };

            response = EditStationResponse(response);

            if (Convert.ToBoolean(response.Valid))
            {
                    CacheResponse(response);
            }

            return response;
        }
        
        private Station DeserializeObservationDocument(XmlDocument doc)
        {
            Observation response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Observation));
            using (XmlReader reader = new XmlNodeReader(doc))
            {
                response = (Observation)serializer.Deserialize(reader);
            }
            return response.Station;
        }
        private Station EditStationResponse(Station response)
        {
            if (response.Vindhradi == response.MestiVindradi)
                response.MestiVindradi = string.Empty;

            response.Vindstefna = _windDirection.GetWindDirection(response.Vindstefna);
            return response;
        }

        private IEnumerable<Station> StationCache()
        {
            var objectInCache = _applicationCache.Get("stations") as IEnumerable<Station>;
            return objectInCache;
        }
        private Station CheckIfStationIsInCache(string stationId)
        {
            var stations = StationCache();
            Station station = (from c in stations
                     where c.Id == stationId
                     select c).FirstOrDefault();
            return station;
        }
        private void CacheResponse(Station response)
        {
            var oldStation = (from st in StationCache()
                              where st.Id == response.Id
                              select st).FirstOrDefault();
            if (oldStation == null || oldStation.Time != response.Time)
            {
                List<Station> list = new List<Station>();
                list.Add(response);
                IEnumerable<Station> newList = list;
                var stations = StationCache().Where(u => u.Id != response.Id).ToList();
                IEnumerable<Station> together = stations.Concat(newList);
                var result = stations.Union(newList);
                var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(75) };
                _applicationCache.Remove("stations");
                _applicationCache.Add("stations", result, policy);
            }
        }
        private bool CheckCachedStationAge(Station station)
        {
            DateTime parsed;
            if (DateTime.TryParse(station.Time, out parsed))
                parsed = DateTime.Parse(station.Time);

            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(parsed);
            Console.WriteLine("Time Difference (minutes): " + span.Minutes);
            Console.WriteLine("Time Difference (hours): " + span.Hours);
            if (span.Hours >= 1 && span.Minutes > 5)
                return false;
            else
                return true;
        }

    }
}
