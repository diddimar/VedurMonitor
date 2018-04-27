using System;
using System.Windows;
using System.Windows.Controls;
using WeatherMonitorClassLibrary;
using WeatherMonitorClassLibrary.Models.XmlResponses;

namespace WeatherMonitor2018.UserControls
{
    /// <summary>
    /// Interaction logic for StationResponseControl.xaml
    /// </summary>
    public partial class StationResponseControl : UserControl
    {
        public StationResponseControl()
        {
            InitializeComponent();
        }

        public void FillTextBoxes(Station response)
        {
            DateTime parsed;
            if (DateTime.TryParse(response.Time, out parsed))
                parsed = DateTime.Parse(response.Time);
            timiTextBox.Text = parsed.ToString("dd MMMM HH:MM");
            stationName.Text = response.Name;
            hitiTextBox.Text = response.Hiti;
            vindstefnaTextBox.Text = response.Vindstefna;
            vindhradiTextBox.Text = response.Vindhradi;
            mestiVindhradiTextBox.Text = response.MestiVindradi;
            vindhvidaTextBox.Text = response.MestaVindhvida;
            urkomaTextBox.Text = response.Urkoma;
            vedurlysingTextBox.Text = response.Vedurlysing;
            skyggniTextBox.Text = response.Skyggni;
            villaTextBox.Text = response.Err;
            SetTextVisibility();
        }

        private void SetTextVisibility()
        {
            foreach (FrameworkElement element in responseFields.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel panel = (StackPanel)element;
                    foreach (TextBox textBox in panel.Children)
                    {
                        if (textBox.Uid == "res")
                        {
                            if (Utils.IsStringEmpty(textBox.Text))
                            {
                                CollapseAll(panel);
                            }
                            else
                            {
                                ShowAll(panel);
                            }
                        }
                    }
                }
            }
        }
        private void CollapseAll(StackPanel panel)
        {
            foreach (TextBox box in panel.Children)
            {
                box.Visibility = Visibility.Collapsed;
            }
        }
        private void ShowAll(StackPanel panel)
        {
            foreach (TextBox box in panel.Children)
            {
                box.Visibility = Visibility.Visible;
            }
        }

    }
}
