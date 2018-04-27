using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherMonitor2018.Data.Models;

namespace WeatherMonitor2018.UserControls
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
            altitudeTextBox.Text = Convert.ToInt32(selectedStation.Altitude).ToString() + " metrar";
            ownerTextBox.Text = "Eigandi: " + selectedStation.Owner;
        }
    }
}
