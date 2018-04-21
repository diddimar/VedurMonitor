using System.Linq;

namespace WeatherMonitorClassLibrary
{
    public static class Utils
    {
        public static bool ArrayInBounds(int index, string[] array)
        {
            return (index >= 0) && (index < array.Count());
        }
    }
}
