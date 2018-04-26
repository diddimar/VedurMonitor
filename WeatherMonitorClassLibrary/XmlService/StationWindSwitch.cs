using System;
namespace WeatherMonitorClassLibrary.XmlService
{
    public static class StationWindSwitch
    {
        public static string Get(string windDirection)
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
                    return "Norðanátt";
                case "NNA":
                    return "Norð-norð-austanátt";
                case "ANA":
                    return "Aust-norð-austanátt";
                case "A":
                    return "Austanátt";
                case "ASA":
                    return "Aust-suð-austanátt";
                case "SA":
                    return "Suð-austanátt";
                case "SSA":
                    return "Suð-suð-austanátt";
                case "S":
                    return "Sunnanátt";
                case "SSV":
                    return "Suð-suð-vestanátt";
                case "VSV":
                    return "Vest-suð-vestanátt";
                case "SV":
                    return "Suð-vestanátt";
                case "V":
                    return "Vestanátt";
                case "VNV":
                    return "Vest-norð-vestanátt";
                case "NV":
                    return "Norð-vestanátt";
                case "NNV":
                    return "Norð-norð-vestanátt";
                default:
                    return String.Empty;
            };

        }

        private static string GetEnglish(string vindstefna)
        {
            if (vindstefna == "Logn" || vindstefna == "Calm")
                return "Calm";
            else if (vindstefna == "N")
                return "North";
            else if (vindstefna == "NNA" || vindstefna == "NNE")
                return "North-north-east";
            else if (vindstefna == "ANA" || vindstefna == "ENE")
                return "East-east-north";
            else if (vindstefna == "A" || vindstefna == "E")
                return "East";
            else if (vindstefna == "ASA" || vindstefna == "ESE")
                return "East-south-east";
            else if (vindstefna == "SA" || vindstefna == "SE")
                return "South-east";
            else if (vindstefna == "SSA" || vindstefna == "SSE")
                return "South-south-east";
            else if (vindstefna == "S")
                return "South";
            else if (vindstefna == "SSV" || vindstefna == "SSW")
                return "South-south-west";
            else if (vindstefna == "SV" || vindstefna == "SW")
                return "South-west";
            else if (vindstefna == "VSV" || vindstefna == "WSW")
                return "West-south-west";
            else if (vindstefna == "V" || vindstefna == "W")
                return "West";
            else if (vindstefna == "VNV" || vindstefna == "WNW")
                return "West-north-west";
            else if (vindstefna == "NV" || vindstefna == "NW")
                return "North-west";
            else if (vindstefna == "NNV" || vindstefna == "NNW")
                return "North-north-west";
            else
                return string.Empty;
        }
    }

}
