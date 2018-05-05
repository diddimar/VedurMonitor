using System;

namespace WeatherMonitor2018.Config
{
    public static class Constants
    {
        public static readonly Uri AssetsRoot = new Uri("pack://application:,,,/VedurMonitor;component/Assets/");
        public static readonly string AboutHttpAndCache =
            $"Þar sem Veðurstofan Íslands uppfærir gögnin sín einu sinni á klukkustund eru allar gildar veðurathuganir " +
            $"sem berast geymdar í skyndiminni forritsins þar til rétt rúmur klukkutími hefur liðið frá því athugunin var gerð.";
    }
}