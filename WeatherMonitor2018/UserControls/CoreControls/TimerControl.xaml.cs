using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WeatherMonitor2018.UserControls.CoreControls
{
    public partial class TimerControl : UserControl
    {
        public TimerControl()
        {
            klukka.Text = DateTime.Now.ToString("HH:mm:ss"); // Starta klukku - Svo update 2svar á min
            InitializeComponent();
            StartClock();
        }

        private void StartClock()
        {
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _timer.Interval = new TimeSpan(0, 0, 30);
            _timer.Start();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            klukka.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
