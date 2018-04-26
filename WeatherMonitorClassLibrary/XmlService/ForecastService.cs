using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WeatherMonitorClassLibrary.Models.XmlResponses;

namespace WeatherMonitorClassLibrary.XmlService
{
    public static class ForecastService
    {
        public static Forecast GetForecast(string selectionId)
        {
            string path = @"http://xmlweather.vedur.is/?op_w=xml&type=txt&lang=is&view=xml&ids=" + selectionId;
            XmlDocument document = Utils.GetXmlDocument(path);
            string forecastXml = document.InnerXml;
            Forecast response = DeserializeForecastXMLText(forecastXml);
            
            if (response == null )
            { return new Forecast { }; }

            return EditForecast(response);
            
        }
        private static Forecast DeserializeForecastXMLText(string forecastXml)
        {
            XmlSerializer serializers = new XmlSerializer(typeof(ForecastRoot));
            ForecastRoot response = null;
            forecastXml = forecastXml.Replace("<br />", "");
            using (StringReader reader = new StringReader(forecastXml))
            {
                response = (ForecastRoot)(serializers.Deserialize(reader));
            }
            return response.Forecast;
        }
        private static Forecast EditForecast(Forecast forecast)
        {
            if (string.IsNullOrEmpty(forecast.Content))
            {
                forecast.Content = "Gögn ekki tiltæk einsog er.";
                forecast.Creation = "";
            }
            return forecast;
        }
    }
}
