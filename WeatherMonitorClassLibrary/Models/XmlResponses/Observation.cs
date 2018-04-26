using System.Xml.Serialization;

namespace WeatherMonitorClassLibrary.Models.XmlResponses
{
    [XmlRoot(ElementName = "observations")]
    public class Observation
    {
        [XmlElement(ElementName = "station", Type = typeof(Station))]
        public Station Station { get; set; }
    }
}