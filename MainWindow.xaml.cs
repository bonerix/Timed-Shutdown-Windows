using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Timed_Shutdown
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        { 
            DragMove(); 
        }

        private void tbPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text))
                e.Handled = true;
        }

        private void tbPasting(object sender, DataObjectPastingEventArgs e) 
        { 
            e.Handled = true;
            e.CancelCommand();
        }

        private void HoursUp(object sender, RoutedEventArgs e)
        {
            if (tbHours.Text != "99")
                tbHours.Text = (byte.Parse(tbHours.Text) + 1).ToString();
        }

        private void HoursDown(object sender, RoutedEventArgs e)
        {
            if (tbHours.Text != "0")
                tbHours.Text = (byte.Parse(tbHours.Text) - 1).ToString();
        }

        private void MinutesUp(object sender, RoutedEventArgs e)
        {
            if (tbMinutes.Text != "59")
                tbMinutes.Text = (byte.Parse(tbMinutes.Text) + 1).ToString();

            else
            {
                tbMinutes.Text = "0";
                tbHours.Text = (byte.Parse(tbHours.Text) + 1).ToString();
            }
        }

        private void MinutesDown(object sender, RoutedEventArgs e)
        {
            if (tbMinutes.Text != "0")
                tbMinutes.Text = (byte.Parse(tbMinutes.Text) - 1).ToString();

            else if (tbHours.Text != "0")
            {
                tbHours.Text = (byte.Parse(tbHours.Text) - 1).ToString();
                tbMinutes.Text = "59";
            }
        }

        private void SecondsUp(object sender, RoutedEventArgs e)
        {
            if (tbSeconds.Text != "59")
                tbSeconds.Text = (byte.Parse(tbSeconds.Text) + 1).ToString();

            else if (tbMinutes.Text != "59")
            {
                tbSeconds.Text = "0";
                tbMinutes.Text = (byte.Parse(tbMinutes.Text) + 1).ToString();
            }

            else if (tbHours.Text != "99")
            {
                tbSeconds.Text = "0";
                tbMinutes.Text = "0";
                tbHours.Text = (byte.Parse(tbHours.Text) + 1).ToString();
            }
        }

        private void SecondsDown(object sender, RoutedEventArgs e)
        {
            if (tbSeconds.Text != "0")
                tbSeconds.Text = (byte.Parse(tbSeconds.Text) - 1).ToString();

            else if (tbMinutes.Text != "0")
            {
                tbMinutes.Text = (byte.Parse(tbMinutes.Text) - 1).ToString();
                tbSeconds.Text = "59";
            }

            else if (tbHours.Text != "0")
            {
                tbHours.Text = (byte.Parse(tbHours.Text) - 1).ToString();
                tbMinutes.Text = "59";
                tbSeconds.Text = "59";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = @"C:\Windows\System32\cmd.exe";
            proc.Arguments = "/c " + "shutdown /a";
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc);
            Close();
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = @"C:\Windows\System32\cmd.exe";
            proc.Arguments = "/c " + "shutdown /s /t " + (byte.Parse(tbHours.Text) * 3600 + byte.Parse(tbMinutes.Text) * 60 + byte.Parse(tbSeconds.Text)).ToString();
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc);
        }

        private void tbHoursFocus(object sender, RoutedEventArgs e)
        {
            if (tbHours.Text == "0")
                tbHours.Text = "";
        }

        private void tbHoursLostFocus(object sender, RoutedEventArgs e)
        {
            if (tbHours.Text == "")
                tbHours.Text = "0";
        }

        private void tbMinutesFocus(object sender, RoutedEventArgs e)
        {
            if (tbMinutes.Text == "0")
                tbMinutes.Text = "";
        }

        private void tbMinutesLostFocus(object sender, RoutedEventArgs e)
        {
            if (tbMinutes.Text == "")
                tbMinutes.Text = "0";
        }

        private void tbSecondsFocus(object sender, RoutedEventArgs e)
        {
            if (tbSeconds.Text == "0")
                tbSeconds.Text = "";
        }

        private void tbSecondsLostFocus(object sender, RoutedEventArgs e)
        {
            if (tbSeconds.Text == "")
                tbSeconds.Text = "0";
        }
    }
}
