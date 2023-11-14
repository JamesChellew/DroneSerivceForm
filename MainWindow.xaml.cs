using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DroneServiceForm
{
    /// <summary>
    /// James Chellew # 30071566
    /// C# Assessment task 3 - Icarus Drone Service Form.
    /// 9/11/2023
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxTag.TextBox.Text = nextServiceTag.ToString();
        }
        // 6.2	Create a global List<T> of type Drone called “completedList”. 
        List<Drone> completedList = new();
        // 6.3	Create a global Queue<T> of type Drone called “RegularService”.
        Queue<Drone> RegularService = new();
        // 6.4	Create a global Queue<T> of type Drone called “ExpressService”.
        Queue<Drone> ExpressService = new();

        // Keeping track of service tag
        private static int nextServiceTag = 100;

        // 6.5 Create a button method called “AddNewItem” that will add a new service item to a Queue<> based on the priority.
        // Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost. Use a numeric control for the Service Tag.
        // The new service item will be added to the appropriate Queue based on the Priority radio button.
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (InformationEntered())
            {
                Drone newDrone = new();
                newDrone.SetName(TextBoxName.Text);
                newDrone.SetModel(TextBoxModel.Text);
                newDrone.SetIssue(TextBoxIssue.Text);
                newDrone.SetTag(int.Parse(TextBoxTag.TextBox.Text)); // Can only be an int 100-900, safe to Parse
                newDrone.SetCost(double.Parse(TextBoxCost.Text)); // Can only be a double value, safe to Parse
                string servicePrioryType = GetServicePriority();

                string nextServiceTag = IncrementServiceTag(TextBoxTag.TextBox.Text); // The next Tag is Caculated before adding to queue

                if (servicePrioryType == "Regular")
                {
                    RegularService.Enqueue(newDrone);
                    DisplayRegularQueue();
                    TextBoxFeedback.Text = "Regular service added";
                }
                else if (servicePrioryType == "Express")
                {
                    ExpressService.Enqueue(newDrone);
                    DisplayExpressQueue();
                    TextBoxFeedback.Text = "Express service added";
                }
                else
                {
                    MessageBox.Show("Select a queue type", "Invalid Queue", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TextBoxFeedback.Text = "No queue type selected";
                    return;
                }
                ClearTextBoxes();
                TextBoxTag.TextBox.Text = nextServiceTag; // Automatically fills the next service tag
            }
            else
            {
                MessageBox.Show("Please Enter All Service Information", "Information Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextBoxFeedback.Text = "Service information missing";
            }
        }
        // Checks all controls for a value.
        private bool InformationEntered() 
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text) ||
                string.IsNullOrWhiteSpace(TextBoxModel.Text) ||
                string.IsNullOrWhiteSpace(TextBoxIssue.Text) ||
                string.IsNullOrWhiteSpace(TextBoxTag.TextBox.Text) ||
                string.IsNullOrWhiteSpace(TextBoxServiceFee.Text))
            {
                return false;
            }
            else { return true; }
        }

        // 6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
        private void CalculateTotalCost(string queueType)
        {
            double serviceFee = double.Parse(TextBoxServiceFee.Text); // safe to parse, Regex in use to prevent non-double values.
            if (queueType == "Express")
            {
                TextBoxCost.Text = (serviceFee * 1.15).ToString("F2");
            }
            else
            {
                TextBoxCost.Text = serviceFee.ToString("F2");
            }
        }
        // Calculates total cost when checking Regular Radio, Gets the selection for correct calc
        private void RadioButtonRegular_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost(GetServicePriority());
            TextBoxFeedback.Text = "Total cost calculated";
        }
        // Calculates total cost when checking Express Radio, Gets the selection for correct calc
        private void RadioButtonExpress_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost(GetServicePriority());
            TextBoxFeedback.Text = "Total cost calculated";
        }

        // 6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group.
        // This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        private string GetServicePriority()
        {
            if (RadioButtonRegular.IsChecked == true)
            {
                return "Regular";
            }
            else if (RadioButtonExpress.IsChecked == true)
            {
                return "Express";
            }
            else
            {
                return "";
            }
        }

        // 6.8 Create a custom method that will display all the elements in the RegularService queue.
        // The display must use a List View and with appropriate column headers.
        private void DisplayRegularQueue()
        {
            ListViewRegularQueue.Items.Clear();
            foreach (Drone dr in RegularService)
            {
                ListViewRegularQueue.Items.Add(new
                { RegularName = dr.GetName(), RegularModel = dr.GetModel(), RegularTag = dr.GetTag(), RegularCost = dr.GetCost().ToString("F2") });
            }
        }
        // 6.9	Create a custom method that will display all the elements in the ExpressService queue.
        // The display must use a List View and with appropriate column headers.
        private void DisplayExpressQueue()
        {
            ListViewExpressQueue.Items.Clear();
            foreach (Drone dr in ExpressService)
            {
                ListViewExpressQueue.Items.Add(new
                { ExpressName = dr.GetName(), ExpressModel = dr.GetModel(), ExpressTag = dr.GetTag(), ExpressCost = dr.GetCost().ToString("F2") });
            }
        }
        // 6.10 Create a custom method to ensure the Service Cost textbox can only accept a double value
        // with two decimal points.
        private void CheckServiceFeeValue()
        {
            Regex rx = new Regex(@"^\d{1,6}(\.(\d{1,2})?)?$");
            
            bool isCorrectFormat = rx.IsMatch(TextBoxServiceFee.Text);
            if (isCorrectFormat)
            {
                double serviceFee = double.Parse(TextBoxServiceFee.Text);
                TextBoxServiceFee.Text = serviceFee.ToString("F2"); // fills the rest of number to 2 dp
                TextBlockErrorMessage.Visibility = Visibility.Hidden; // Hides message once fee is in correct format
            }
            else
            {
                TextBoxServiceFee.Text = "0.00";
                TextBlockErrorMessage.Visibility = Visibility.Visible; // Shows message that Service fee is in the wrong format
            }
        }
        // Checks Service Fee for correct formatting and then calculates total cost when the box lost focus
        private void TextBoxServiceFee_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckServiceFeeValue();
            CalculateTotalCost(GetServicePriority());
        }

        // 6.11	Create a custom method to increment the service tag control, this method must be called inside the “AddNewItem” method
        // before the new service item is added to a queue.
        private string IncrementServiceTag(string previousTag)
        {
            nextServiceTag = int.Parse(previousTag) + 10;
            if (nextServiceTag > 900)
            {
                nextServiceTag = 100;
            }
            return nextServiceTag.ToString();
        }
        // 6.12	Create a mouse click method for the regular service ListView that will display the Client Name and Service Problem in the related textboxes.
        private void ListViewRegularQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewRegularQueue.SelectedItems.Count > 0)
            {
                int index = ListViewRegularQueue.SelectedIndex;
                TextBoxName.Text = RegularService.ElementAt(index).GetName();
                TextBoxModel.Text = RegularService.ElementAt(index).GetModel();
                TextBoxIssue.Text = RegularService.ElementAt(index).GetIssue();
                TextBoxTag.TextBox.Text = RegularService.ElementAt(index).GetTag().ToString();
                TextBoxServiceFee.Text = RegularService.ElementAt(index).GetCost().ToString("F2");
                RadioButtonRegular.IsChecked = true;
                RadioButtonExpress.IsChecked = false;
                TextBoxCost.Text = RegularService.ElementAt(index).GetCost().ToString();
                TextBoxFeedback.Text = "A regular service was selected";
            }
        }
        // Removes selection and the info from textboxes when List view loses focus
        private void ListViewRegularQueue_LostFocus(object sender, RoutedEventArgs e)
        {
            ListViewRegularQueue.UnselectAll();
            ClearTextBoxes();
        }

        // 6.13	Create a mouse click method for the express service ListView that will display the Client Name and Service Problem in the related textboxes.
        private void ListViewExpressQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewExpressQueue.SelectedItems.Count > 0)
            {
                int index = ListViewExpressQueue.SelectedIndex;
                TextBoxName.Text = ExpressService.ElementAt(index).GetName();
                TextBoxModel.Text = ExpressService.ElementAt(index).GetModel();
                TextBoxIssue.Text = ExpressService.ElementAt(index).GetIssue();
                TextBoxTag.TextBox.Text = ExpressService.ElementAt(index).GetTag().ToString();
                TextBoxServiceFee.Text = (ExpressService.ElementAt(index).GetCost() / 1.15).ToString("F2");
                RadioButtonRegular.IsChecked = false;
                RadioButtonExpress.IsChecked = true;
                TextBoxCost.Text = ExpressService.ElementAt(index).GetCost().ToString();
                TextBoxFeedback.Text = "An express service was selected";
            }
        }
        // Removes selection and the info from textboxes when List view loses focus
        private void ListViewExpressQueue_LostFocus(object sender, RoutedEventArgs e)
        {
            ListViewExpressQueue.UnselectAll();
            ClearTextBoxes();
        }
        // Displays the the properties of each drone for the completed drone list.
        private void DisplayCompletedList()
        {
            ListBoxCompleted.Items.Clear();
            foreach (Drone dr in completedList)
            {
                ListBoxCompleted.Items.Add($"| {dr.GetTag()} | {dr.GetName()}: {dr.GetModel()} ${dr.GetCost():F2}");
            }
        }

        // 6.14	Create a button click method that will remove a service item from the regular ListView and dequeue the regular service Queue<T> data structure.
        // The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        private void ButtonDequeueRegular_Click(object sender, RoutedEventArgs e)
        {
            if (RegularService.Count > 0)
            {
                completedList.Add(RegularService.Dequeue());
                DisplayRegularQueue();
                DisplayCompletedList();
                TextBoxFeedback.Text = "Regular service completed";
            }
        }
        // 6.15	Create a button click method that will remove a service item from the express ListView and dequeue the express service Queue<T> data structure.
        // The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        private void ButtonDequeueExpress_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
                completedList.Add(ExpressService.Dequeue());
                DisplayExpressQueue();
                DisplayCompletedList();
                TextBoxFeedback.Text = "Express service completed";
            }
        }

        // 6.16	Create a double mouse click method that will delete a service item from the finished listbox and remove the same item from the List<T>.
        private void DeleteCompletedServiceEntry()
        {
            if (ListBoxCompleted.SelectedItems.Count > 0)
            {
                int deleteIndex = ListBoxCompleted.SelectedIndex;
                completedList.RemoveAt(deleteIndex);
                DisplayCompletedList();
                TextBoxFeedback.Text = "Completed service removed";
            }
        }
        // 6.17	Create a custom method that will clear all the textboxes after each service item has been added.
        private void ClearTextBoxes()
        {
            TextBoxName.Clear();
            TextBoxModel.Clear();
            TextBoxIssue.Clear();
            TextBoxTag.TextBox.Text = nextServiceTag.ToString();
            TextBoxServiceFee.Clear();
            RadioButtonRegular.IsChecked = false;
            RadioButtonExpress.IsChecked = false;
            TextBoxCost.Clear();
        }
        // Deletes selection on double click of the item in the ListBox
        private void ListBoxCompleted_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DeleteCompletedServiceEntry();
        }
        // Deletes selection in the ListBox on button click 
        private void ButtonDroneCollected_Click(object sender, RoutedEventArgs e)
        {
            DeleteCompletedServiceEntry();
        }
        // checks the format of the service tag when clicking off textbox
        private void TextBoxTag_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckServiceTag();
        }
        // Custom method that uses a regex to verify Service tag is an int between 100 and 900. Resets to default if not.
        private void CheckServiceTag()
        {
            Regex rx = new Regex(@"^[1-8][0-9]{2}$|900$");
            if (!rx.IsMatch(TextBoxTag.TextBox.Text))
            {
                TextBoxTag.TextBox.Text = nextServiceTag.ToString();
            }
        }
        // Ensures Correct format with each character pressed. Will allow up to 123456.12 (1, 1., 1.1 ,1.12 are also acceptable)
        // Reads the text in textbox and concatenates the Text to be added. If it matches Regex, then don't handle eventargs (text added).
        // If it doesn't match Regex, handle the eventargs "e" (i.e. stop the text from being added)
        private void TextBoxServiceFee_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex rx = new Regex("^\\d{1,6}(\\.(\\d{1,2})?)?$");
            e.Handled = !rx.IsMatch(TextBoxServiceFee.Text + e.Text); // Don't want to handle when correct, so inverting bool
        }
        // Prevent the spacebar from entering white space in TextBoxServiceFee. Handle event when it is pressed.
        private void TextBoxServiceFee_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
