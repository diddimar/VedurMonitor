namespace WeatherMonitorClassLibrary.Models.DbObjects
{
    public class ForecastInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ForecastNumber { get; set; }
    }
}
