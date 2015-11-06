using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TimeSheet.Database.Providers;
using TimeSheet.DataItems;
using TimeSheet.Model;
using TimeSheet.ViewModel;

namespace TimeSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMonthRepository _monthRepository;
        private AppViewModel _mainViewModel;
        private Month _selectedMonth;
        private Day _selectedDay;

        public MainWindow()
        {
            InitializeComponent();

            //_monthRepository = new DbMonthRepository();
            _monthRepository = new FileMonthRepository();

            _mainViewModel = new AppViewModel(_monthRepository);
            DataContext = _mainViewModel;
        }

        #region Data refresh
        private void ReloadData()
        {
            _mainViewModel.ReloadData();
        }

        private void RefreshSummaries()
        {
            if (_selectedMonth == null)
                return;

            RefreshMonthlySummary();
            if (_selectedDay != null)
                RefreshDailySummary();
        }

        private void RefreshMonthlySummary()
        {
            var monthlyMinutesWorked = _selectedMonth.MinutesWorked;
            MonthlySummaryListBox.ItemsSource = new List<KeyValueItem>
            {
                new KeyValueItem { Key = "Total hours worked monthly",  Value = string.Format("{0}h {1}m", (int)monthlyMinutesWorked/60, monthlyMinutesWorked%60) },
                new KeyValueItem { Key = "Estimated salary", Value = string.Format("{0:F} PLN", CalculateEstimatedSalary(monthlyMinutesWorked)) }
            };
        }

        private void RefreshDailySummary()
        {
            var dailyMinutesWorked = _selectedDay.MinutesWorked;
            var weeklyMinutesWorked = _selectedMonth.GetMinutesWorkedWeekly(_selectedDay);

            DailySummaryListBox.ItemsSource = new List<KeyValueItem>
            {
                new KeyValueItem
                {
                    Key = "Total hours worked daily",
                    Value = string.Format("{0}h {1}m", (int)dailyMinutesWorked/60, dailyMinutesWorked%60)
                },
                new KeyValueItem
                {
                    Key = "Total hours worked weekly",
                    Value = string.Format("{0}h {1}m", (int)weeklyMinutesWorked/60, weeklyMinutesWorked%60)
                }
            };
        }
        #endregion

        #region Selection changes
        private void MonthsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedMonth = MonthsComboBox.SelectedItem as Month;
            if (_selectedMonth == null)
                return;

            MonthChanged();
        }

        private void MonthChanged()
        {
            _mainViewModel.Days = _selectedMonth.Days;
            MonthlyDatesListBox.ItemsSource = _mainViewModel.Days;
            
            RefreshMonthlySummary();

            DailyWorkTimePeriodsListBox.ItemsSource = null;
            DailyBreakTimePeriodsListBox.ItemsSource = null;
            DailySummaryListBox.ItemsSource = null;
        }

        private void MonthlyDatesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDay = MonthlyDatesListBox.SelectedItem as Day;
            if (_selectedDay == null)
                return;

            DayChanged();
        }

        private void DayChanged()
        {
            DailyWorkTimePeriodsListBox.ItemsSource = _selectedDay.WorkTimePeriods;
            DailyBreakTimePeriodsListBox.ItemsSource = _selectedDay.BreakTimePeriods;

            RefreshDailySummary();
        }
        #endregion

        #region Buttons
        private void AddNewMonthButon_OnClick(object sender, RoutedEventArgs e)
        {
            var newMonthWindow = new NewMonthWindow();
            newMonthWindow.ShowDialog();
            if (newMonthWindow.DialogResult == true)
            {
                _mainViewModel.Months.Add(new Month(newMonthWindow.NewMonthName)
                {
                    Days = new ObservableCollection<Day>()
                });
            }
        }

        private void NewDayButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_selectedMonth == null)
                return;

            var newDayWindow = new NewDayWindow();
            newDayWindow.ShowDialog();
            if (newDayWindow.DialogResult == true)
            {
                _selectedMonth.Days.Add(new Day(newDayWindow.SelectedDate));
            }
        }

        private void AddNewWorkTimePeriodButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_selectedDay == null)
                return;

            var newTimePeriodWindow = new NewTimePeriodWindow();
            newTimePeriodWindow.ShowDialog();
            if (newTimePeriodWindow.DialogResult == true)
            {
                _selectedDay.WorkTimePeriods.Add(newTimePeriodWindow.SelectedTimePeriod);
            }

            RefreshSummaries();
        }

        private void AddNewBreakTimePeriodButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_selectedDay == null)
                return;

            var newTimePeriodWindow = new NewTimePeriodWindow();
            newTimePeriodWindow.ShowDialog();
            if (newTimePeriodWindow.DialogResult == true)
            {
                _selectedDay.BreakTimePeriods.Add(newTimePeriodWindow.SelectedTimePeriod);
            }

            RefreshSummaries();
        }

        private void DeleteDay(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var day = (Day) button.DataContext;
            _selectedMonth.Days.Remove(day);

            RefreshSummaries();
        }

        private void DeleteWorkTimePeriod(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var timePeriod = (TimePeriod) button.DataContext;
            _selectedDay.WorkTimePeriods.Remove(timePeriod);

            RefreshSummaries();
        }

        private void DeleteBreakTimePeriod(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var timePeriod = (TimePeriod)button.DataContext;
            _selectedDay.BreakTimePeriods.Remove(timePeriod);

            RefreshSummaries();
        }
        #endregion

        private double CalculateEstimatedSalary(double totalMinutesWorked)
        {
            var hourlyWage = 18.0;
            var hours = (int)totalMinutesWorked / 60;
            var minutes = totalMinutesWorked % 60;
            if (minutes >= 15)
                hours += 1;

            return Math.Max(hours * hourlyWage * 0.8515 - 10.0, 0);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _monthRepository.Save(_mainViewModel.Months);
        }
    }
}
