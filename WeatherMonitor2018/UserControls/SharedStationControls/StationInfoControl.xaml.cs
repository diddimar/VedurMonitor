using System;
using System.Windows.Controls;
using WeatherMonitor2018.Data.Models;

namespace WeatherMonitor2018.UserControls.SharedStationControls
{
    /// <summary>
    /// Interaction logic for StationInfoControl.xaml
    /// </summary>
    public partial class StationInfoControl : UserControl
    {
        public StationInfoControl()
        {
            InitializeComponent();
        }

        public void SetStationInfo(StationInfo selectedStation)
        {
            stationType.Text = selectedStation.Type;
            altitudeTextBox.Text = Convert.ToInt32(selectedStation.Altitude).ToString() + " metrar yfir sjávarmáli";
            ownerSinceTextBox.Text = selectedStation.Owner +" "+ selectedStation.UpphafAthuguna.ToString() +" | Nr. "+ selectedStation.StationNumber;
        }
    }
}
