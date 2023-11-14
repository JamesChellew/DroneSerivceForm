using System.Windows;
using System.Windows.Controls;

namespace DroneServiceForm
{
    /// <summary>
    /// Interaction logic for TextBoxIncrementor.xaml
    /// </summary>
    public partial class TextBoxIncrementor : UserControl
    {
        public TextBoxIncrementor()
        {
            InitializeComponent();
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if (IsNextNumberValid())
            {
                TextBox.Text = (int.Parse(TextBox.Text) + 10).ToString();
                ButtonDown.Focus();
                ButtonUp.Focus();
            }
            else
            {
                return;
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if (IsNextNumberValid())
            {
                TextBox.Text = (int.Parse(TextBox.Text) - 10).ToString();
                ButtonUp.Focus();
                ButtonDown.Focus();
            }
            else
            {
                return;
            }
        }
        private bool IsNextNumberValid()
        {
            return int.TryParse(TextBox.Text, out _);
        }
    }
}
