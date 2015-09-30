using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TimeSheet.Database.Providers;
using TimeSheet.DataItems;
using TimeSheet.Model;

namespace TimeSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<Month> _months;
        private Month _selectedMonth;
        private Day _selectedDay;

        public MainWindow()
        {
            InitializeComponent();
            ReloadData();
        }

        private void ReloadData()
        {
            _months = DbMonthProvider.GetAll();
            MonthsComboBox.ItemsSource = _months.Select(m => m.MonthName);
        }

        private void MonthsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMonthName = MonthsComboBox.SelectedItem as string;
            _selectedMonth = _months.First(m => m.MonthName == selectedMonthName);
            var monthlyMinutesWorked = _selectedMonth.GetMinutesWorkedCount();
            MonthlyDatesListBox.ItemsSource = _selectedMonth.Days.Select(d => new ValueItem { Value = d.Date.ToShortDateString() });

            MonthlySummaryListBox.ItemsSource = new List<KeyValueItem>
            {
                new KeyValueItem { Key = "Total hours worked monthly",  Value = string.Format("{0}h {1}m", monthlyMinutesWorked/60, monthlyMinutesWorked%60) },
                new KeyValueItem { Key = "Estimated salary", Value = string.Format("{0:F} PLN", CalculateEstimatedSalary(monthlyMinutesWorked)) }
            };
        }

        private void MonthlyDatesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDateArray =
                (MonthlyDatesListBox.SelectedItem as ValueItem).Value.Split('-').Select(x => Convert.ToInt32(x)).ToArray();
            var selectedDate = new DateTime(selectedDateArray[0], selectedDateArray[1], selectedDateArray[2]);

            _selectedDay = _selectedMonth.Days.First(d => d.Date == selectedDate);
            var dailyMinutesWorked = _selectedDay.GetDailyMinutesWorked();
            DailyTimePeriodsListBox.ItemsSource = _selectedDay.TimePeriods.Select(p => new ValueItem { Value = FormatTimePeriod(p) });

            DailySummaryListBox.ItemsSource = new List<KeyValueItem>
            {
                new KeyValueItem { Key = "Total hours worked daily", Value = string.Format("{0}h {1}m", dailyMinutesWorked/60, dailyMinutesWorked%60)}
            };
        }

        private string FormatTimePeriod(TimePeriod timePeriod)
        {
            return string.Format("{0}:{1} - {2}:{3}",
                DecimateInt(timePeriod.StartTime.Hour),
                DecimateInt(timePeriod.StartTime.Minute),
                DecimateInt(timePeriod.EndTime.Hour),
                DecimateInt(timePeriod.EndTime.Minute));
        }

        private string DecimateInt(int number)
        {
            return number >= 10 ? number.ToString() : string.Format("0{0}", number);
        }

        private double CalculateEstimatedSalary(double totalMinutesWorked)
        {
            var hourlyWage = 18.0;
            var hours = totalMinutesWorked/60;
            var minutes = totalMinutesWorked%60;
            if (minutes >= 30)
                hours += 1.0;

            return Math.Max(hours*hourlyWage*0.8518 - 10.0, 0);
        }
    }
}
