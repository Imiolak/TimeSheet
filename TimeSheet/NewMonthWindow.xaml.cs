using System.Windows;

namespace TimeSheet
{
    /// <summary>
    /// Interaction logic for NewMonthWindow.xaml
    /// </summary>
    public partial class NewMonthWindow : Window
    {
        public string NewMonthName
        {
            get { return NewMonthNameTextBox.Text; }
        }

        public NewMonthWindow()
        {
            InitializeComponent();
        }

        private void ConfirmAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = NewMonthNameTextBox.Text != string.Empty;
            Close();
        }
    }
}
