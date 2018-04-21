using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMonitorClassLibrary
{
    public class MapImageResolver
    {
        public string[] SetObservationMap(int landshlutaIndex)
        {
            switch (landshlutaIndex)
            {
                default:
                    return new string[] { "fa" };
                case 1:
                    return new string[] { "br" };
                case 2:
                    return new string[] { "vf" };
                case 3:
                    return new string[] { "nv" };
                case 4:
                    return new string[] { "na" };
                case 5:
                    return new string[] { "al" };
                case 6:
                    return new string[] { "af" };
                case 7:
                    return new string[] { "sa", "sa_allar" };
                case 8:
                    return new string[] { "su" };
                case 9:
                    return new string[] { "mh" };
            }
        }
        
        public string SetStationIndicator(int landshlutaIndex, int stationIndex)
        {
            switch (landshlutaIndex)
            {
                default:
                    return string.Empty;
                case 0:
                    return string.Empty;
                case 1:
                    return string.Empty;
                case 2:
                    return string.Empty;
                case 3:
                    return string.Empty;
                case 4:
                    return string.Empty;
                case 5:
                    return string.Empty;
                case 6:
                    return string.Empty;
                case 7:
                    return SaIndicators(stationIndex);
                case 8:
                    return string.Empty;
                case 9:
                    return string.Empty;
            }
        }

        private string SaIndicators(int stationIndex)
        {
            switch (stationIndex)
            {
                default:
                    return "skardsfjviti";
                case 0:
                    return "fagurholsm";
                case 1:
                    return "gigjukvisl";
                case 2:
                    return "hofnihorna";
                case 3:
                    return "ingolfshofdi";
                case 4:
                    return "kirkjuklaust";
                case 5:
                    return "kvisker";
                case 6:
                    return "kvisker_veg";
                case 7:
                    return "lomagnupur";
                case 8:
                    return "myrdalssandur";
                case 9:
                    return "oraefi";
                case 10:
                    return "reynisfjall";
                case 11:
                    return "skaftafell";
            }
            

        }

    }
}
