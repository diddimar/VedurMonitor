using System;
namespace WeatherMonitorClassLibrary
{
    public class WindDirection
    {
        public string GetWindDirection(string windDirection)
        {
            if (String.IsNullOrEmpty(windDirection))
            {
                return String.Empty;
            }
            switch (windDirection)
            {
                case "Logn":
                    return "Logn";
                case "N":
                    return "Vindátt: Norðanátt";
                case "NNA":
                    return "Vindátt: Norð-norð-austanátt";
                case "ANA":
                    return "Vindátt: Aust-norð-austanátt";
                case "A":
                    return "Vindátt: Austanátt";
                case "ASA":
                    return "Vindátt: Aust-suð-austanátt";
                case "SA":
                    return "Vindátt: Suð-austanátt";
                case "SSA":
                    return "Vindátt: Suð-suð-austanátt";
                case "S":
                    return "Vindátt: Sunnanátt";
                case "SSV":
                    return "Vindátt: Suð-suð-vestanátt";
                case "VSV":
                    return "Vindátt: Vest-suð-vestanátt";
                case "SV":
                    return "Vindátt: Suð-vestanátt";
                case "V":
                    return "Vindátt: Vestanátt";
                case "VNV":
                    return "Vindátt: Vest-norð-vestanátt";
                case "NV":
                    return "Vindátt: Norð-vestanátt";
                case "NNC":
                    return "Vindátt: Norð-norð-vestanátt";
                default:
                    return String.Empty;
            };

        }
    }
}
