
namespace WeatherMonitorClassLibrary.ImageService
{
    public static class ForecastImageResolver
    {
        public static string GetMap(int textStationNumber)
        {
            switch (textStationNumber)
            {
                default:
                    return "allt";
                case 3:
                    return "phb";
                case 30:
                    return "pmh";
                case 31:
                    return "psu";
                case 32:
                    return "pfa";
                case 33:
                    return "pbr";
                case 34:
                    return "pvf";
                case 35:
                    return "pnv";
                case 36:
                    return "pna";
                case 37:
                    return "pal";
                case 38:
                    return "paf";
                case 39:
                    return "psa";
            }
        }
        


    }
}
