using System.Xml.Serialization;

namespace WeatherMonitorClassLibrary.Models.XmlResponses
{
    [XmlRoot(ElementName = "text")]
    public class Forecast
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "creation")]
        public string Creation { get; set; }
        [XmlElement(ElementName = "valid_from")]
        public string Valid_from { get; set; }
        [XmlElement(ElementName = "valid_to")]
        public string Valid_to { get; set; }
        [XmlElement(ElementName = "content")]
        public string Content { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "texts")]
    public class ForecastRoot
    {
        [XmlElement(ElementName = "text")]
        public Forecast Forecast { get; set; }
    }

}