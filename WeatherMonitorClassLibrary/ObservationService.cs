using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using WeatherMonitorClassLibrary.Models;

namespace WeatherMonitorClassLibrary
{
    public class ObservationService
    {
        WindDirection _windDirection;
        public ObservationService()
        {
            _windDirection = new WindDirection();
        }
        public Station GetStationObservation(string stationId)
        {
            string path = @"http://xmlweather.vedur.is/?op_w=xml&type=obs&lang=is&view=xml&ids=" + stationId;
            XmlDocument document = Utils.GetXmlDocument(path);
            Station response = DeserializeObservationDocument(document);

            if (response == null)
            { return new Station { Valid= 0, Err = "Villa. Reynið aftur seinna..." }; }

            return EditStationResponse(response);
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
            DateTime parsed;
            if (DateTime.TryParse(response.Time, out parsed))
                parsed = DateTime.Parse(response.Time);
            response.Time = parsed.ToString("dd MMMM HH:MM");

            if (response.Vindhradi == response.MestiVindradi)
                response.MestiVindradi = string.Empty;

            response.Vindstefna = _windDirection.GetWindDirection(response.Vindstefna);
            return response;
        }

    }
}
