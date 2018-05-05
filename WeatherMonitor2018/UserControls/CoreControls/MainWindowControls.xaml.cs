using System.Windows;
using System.Windows.Controls;

namespace WeatherMonitor2018.UserControls.CoreControls
{
    /// <summary>
    /// Interaction logic for MainWindowControls.xaml
    /// </summary>
    public partial class MainWindowControls : UserControl
    {
        public WindowState MainWindow { get; private set; }

        public MainWindowControls()
        {
            InitializeComponent();
        }

        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
