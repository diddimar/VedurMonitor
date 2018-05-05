using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WeatherMonitorClassLibrary.Models;
using WeatherMonitorClassLibrary.Models.XmlResponses;

namespace WeatherMonitorClassLibrary.XmlService
{
    public static class StationService
    {
        static ObjectCache _applicationCache = MemoryCache.Default;
        public static int cacheCount = 0;
        public static int httpCount = 0;

        public static Station Get(string stationId)
        {
            var station = CheckIfStationIsInCache(stationId);
            if (station != null && CheckCachedStationAge(station))
            {
                cacheCount++;
                UpdateCacheInfoCache(cacheCount);
                return station;
            }
            else
            {
                httpCount++;
                UpdateCacheInfoHttp(httpCount);
                return GetXML(stationId);
            }
        }
        public static void UpdateCacheInfoCache(int count)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(75) };
            _applicationCache.Remove("StationsCacheCount");
            var info = new InfoCacheModel() { Description = "Sótt frá skyndimynni: ", Value = count.ToString() };
            _applicationCache.Add("StationsCacheCount", info, policy);
        }
        public static void UpdateCacheInfoHttp(int count)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(75) };
            _applicationCache.Remove("StationsHttpCount");
            var info = new InfoCacheModel() { Description = "Http köll til veðurstofunar: ", Value = count.ToString() };
            _applicationCache.Add("StationsHttpCount", info, policy);
        }
        private static Station GetXML(string stationId)
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
        
        private static Station DeserializeObservationDocument(XmlDocument doc)
        {
            Observation response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Observation));
            using (XmlReader reader = new XmlNodeReader(doc))
            {
                response = (Observation)serializer.Deserialize(reader);
            }
            return response.Station;
        }
        private static Station EditStationResponse(Station response)
        {
            if (response.Vindhradi == response.MestiVindradi)
                response.MestiVindradi = string.Empty;

            response.Vindstefna = StationWindSwitch.Get(response.Vindstefna);
            return response;
        }

        private static IEnumerable<Station> StationCache()
        {
            var stationsInCache = _applicationCache.Get("stations") as IEnumerable<Station>;
            if(stationsInCache != null)
                return stationsInCache;
            else
            {
                Station defaultStation = new Station { Time = "Nothing", Hiti = "0", Vedurlysing = "" };
                List<Station> list = new List<Station>();
                list.Add(defaultStation);
                list.Add(defaultStation);
                IEnumerable<Station> en = list;
                return en;
            }
        }
        private static Station CheckIfStationIsInCache(string stationId)
        {
            var stations = StationCache();
            Station station = (from c in stations
                     where c.Id == stationId
                     select c).FirstOrDefault();
            return station;
        }
        private static void CacheResponse(Station response)
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
        private static bool CheckCachedStationAge(Station station)
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
