using System;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeSheet.Model;

namespace TimeSheet
{
    /// <summary>
    /// Interaction logic for NewTimePeriodWindow.xaml
    /// </summary>
    public partial class NewTimePeriodWindow : Window
    {
        public TimePeriod SelectedTimePeriod
        {
            get { return CreateTimePeriodFromInput(); }
        }

        public NewTimePeriodWindow()
        {
            InitializeComponent();
        }

        private TimePeriod CreateTimePeriodFromInput()
        {
            return new TimePeriod
            {
                StartTime = new DateTime(2000, 1, 1, int.Parse(StartTimeHourTB.Text), int.Parse(StartTimeMinuteTB.Text), 0),
                EndTime = new DateTime(2000, 1, 1, int.Parse(EndTimeHourTB.Text), int.Parse(EndTimeMinuteTB.Text), 0)
            };
        }

        private void AddNewTimePeriodButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        #region Allow Only Numbers
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((sender as TextBox).Text.Length >= 2)
                e.Handled = true;
            else 
                e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^([\d]{1})$");
            return regex.IsMatch(text);
        }
        #endregion
    }
}
