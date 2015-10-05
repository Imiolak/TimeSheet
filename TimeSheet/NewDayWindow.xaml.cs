using System;
using System.Windows;

namespace TimeSheet
{
    /// <summary>
    /// Interaction logic for NewDayWindow.xaml
    /// </summary>
    public partial class NewDayWindow : Window
    {
        public DateTime SelectedDate
        {
            get { return NewDateDatePicker.SelectedDate.Value.Date; }
        }

        public NewDayWindow()
        {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = NewDateDatePicker.SelectedDate != null;
            Close();
        }
    }
}
