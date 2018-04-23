using System;
using System.Linq;

namespace WeatherMonitorClassLibrary
{
    public static class Utils
    {
        public static bool ArrayInBounds(int index, string[] array)
        {
            return (index >= 0) && (index < array.Count());
        }
        public static string Truncate(this string str, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
            }
            if (str == null)
            {
                return null;
            }

            int maxLength = Math.Min(str.Length, length);
            return str.Substring(0, maxLength);
        }
    }
}
