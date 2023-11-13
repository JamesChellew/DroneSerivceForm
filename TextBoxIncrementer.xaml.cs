using System.Windows;
using System.Windows.Controls;

namespace DroneSerivceForm
{
    /// <summary>
    /// Interaction logic for TextBoxIncrementer.xaml
    /// </summary>
    public partial class TextBoxIncrementer : UserControl
    {
        public TextBoxIncrementer()
        {
            InitializeComponent();
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            CheckForInvalidNumber();
            TextBox.Text = (int.Parse(TextBox.Text) + 10).ToString();
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            CheckForInvalidNumber();
            TextBox.Text = (int.Parse(TextBox.Text) - 10).ToString();
        }

        //private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    Regex rx = new ("^[0-9]*$");
        //    if (!rx.IsMatch(TextBox.Text))
        //    {
        //        TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1, 1);
        //    }
        //}

        private void CheckForInvalidNumber()
        {
            if (string.IsNullOrWhiteSpace(TextBox.Text))
            {
                TextBox.Text = "0";
            }
            else if (!int.TryParse(TextBox.Text, out int value))
            {
                TextBox.Text = "0";
            }
        }
    }
}
