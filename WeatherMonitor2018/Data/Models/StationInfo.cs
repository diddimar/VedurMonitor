namespace WeatherMonitor2018.Data.Models
{
    public class StationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int WMO_Number { get; set; }
        public string Shortname { get; set; }
        public int Region { get; set; }
        public string Location { get; set; }
        public float Altitude { get; set; }
        public int UpphafAthuguna { get; set; }
        public string Owner { get; set; }
        public int StationNumber { get; set; }
    }
}
