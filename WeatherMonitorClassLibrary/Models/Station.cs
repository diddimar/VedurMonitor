using System.Xml.Serialization;

namespace WeatherMonitorClassLibrary.Models
{

    [XmlRoot(ElementName = "station")]
    public class Station
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("valid")]
        public int Valid { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "time")]
        public string Time { get; set; }

        [XmlElement(ElementName = "err")]
        public string Err { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "F")]
        public string Vindhradi { get; set; }

        [XmlElement(ElementName = "D")]
        public string Vindstefna { get; set; }

        [XmlElement(ElementName = "FX")]
        public string FX { get; set; }

        [XmlElement(ElementName = "FG")]
        public string FG { get; set; }

        [XmlElement(ElementName = "T")]
        public string Hiti { get; set; }

        [XmlElement(ElementName = "W")]
        public string Vedurlysing { get; set; }

        [XmlElement(ElementName = "V")]
        public string V { get; set; }

        [XmlElement(ElementName = "R")]
        public string R { get; set; }
    }
}
