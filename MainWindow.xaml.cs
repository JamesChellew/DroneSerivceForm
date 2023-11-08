﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace DroneSerivceForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxTag.Text = "100";
        }
        // 6.2	Create a global List<T> of type Drone called “FinishedList”. 
        List<Drone> FinishedList = new();
        //6.3	Create a global Queue<T> of type Drone called “RegularService”.
        Queue<Drone> RegularQueue = new();
        //6.4	Create a global Queue<T> of type Drone called “ExpressService”.
        Queue<Drone> ExpressQueue = new();


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
                newDrone.SetTag(int.Parse(TextBoxTag.Text)); // Change this later
                newDrone.SetCost(double.Parse(TextBoxCost.Text)); // Change this later
                string servicePrioryType = GetServicePriority();
                if (servicePrioryType == "Regular")
                {
                    RegularQueue.Enqueue(newDrone);
                    DisplayRegularQueue();
                }
                else if (servicePrioryType == "Express")
                {
                    ExpressQueue.Enqueue(newDrone);
                    DisplayExpressQueue();
                }
                else
                {
                    MessageBox.Show("Select a queue type", "Invalid Queue", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                string nextServiceTag = IncrementServiceTag(TextBoxTag.Text);
                ClearTextBoxes();
                TextBoxTag.Text = nextServiceTag;
            }
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
        private bool InformationEntered()
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text) ||
                string.IsNullOrWhiteSpace(TextBoxModel.Text) ||
                string.IsNullOrWhiteSpace(TextBoxIssue.Text) ||
                string.IsNullOrWhiteSpace(TextBoxTag.Text) ||
                string.IsNullOrWhiteSpace(TextBoxServiceFee.Text))
            {
                return false;
            }
            else { return true; }
        }
        // 6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
        private void CalculateTotalCost(string queueType)
        {
            if (queueType == "Express")
            {
                bool tryConvertCost = double.TryParse(TextBoxServiceFee.Text, out double totalCost);
                if (tryConvertCost)
                {
                    TextBoxCost.Text = (totalCost*1.15).ToString("F2");
                    return;
                }
            }
            else
            {
                TextBoxCost.Text = TextBoxServiceFee.Text;
            }
        }
        private void RadioButtonRegular_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost(GetServicePriority());
        }

        private void RadioButtonExpress_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost(GetServicePriority());
        }
        // 6.8 Create a custom method that will display all the elements in the RegularService queue.
        // The display must use a List View and with appropriate column headers.
        private void DisplayRegularQueue()
        {
            ListViewRegularQueue.Items.Clear();
            foreach (Drone dr in RegularQueue)
            {
                ListViewRegularQueue.Items.Add(new
                { RegularName = dr.GetName(), RegularModel = dr.GetModel(), RegularTag = dr.GetTag(), RegularCost = dr.GetCost() });
            }
        }
        // 6.9	Create a custom method that will display all the elements in the ExpressService queue.
        // The display must use a List View and with appropriate column headers.
        private void DisplayExpressQueue()
        {
            ListViewExpressQueue.Items.Clear();
            foreach (Drone dr in ExpressQueue)
            {
                ListViewExpressQueue.Items.Add(new
                { ExpressName = dr.GetName(), ExpressModel = dr.GetModel(), ExpressTag = dr.GetTag(), ExpressCost = dr.GetCost() });
            }
        }
        // 6.10 Create a custom method to ensure the Service Cost textbox can only accept a double value
        // with two decimal points.
        private void CheckServiceFeeValue()
        {
            bool isNumber = double.TryParse(TextBoxServiceFee.Text, out double serviceFee);
            if (isNumber)
            {
                TextBoxServiceFee.Text = serviceFee.ToString("F2");
                TextBlockErrorMessage.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBoxServiceFee.Clear();
                TextBlockErrorMessage.Visibility = Visibility.Visible;
            }
        }
        private void TextBoxServiceFee_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckServiceFeeValue();
            CalculateTotalCost(GetServicePriority());
        }

        // 6.11	Create a custom method to increment the service tag control, this method must be called inside the “AddNewItem” method
        // before the new service item is added to a queue.
        private string IncrementServiceTag(string previousTag)
        {
            return (int.Parse(previousTag) + 50).ToString();
        }

        private void ClearTextBoxes()
        {
            TextBoxName.Clear();
            TextBoxModel.Clear();
            TextBoxIssue.Clear();
            TextBoxTag.Clear();
            TextBoxServiceFee.Clear();
            RadioButtonRegular.IsChecked = false;
            RadioButtonExpress.IsChecked = false;
            TextBoxCost.Clear();
        }
        // 6.12	Create a mouse click method for the regular service ListView that will display the Client Name and Service Problem in the related textboxes.
        private void ListViewRegularQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewRegularQueue.SelectedItems.Count > 0)
            {
                int index = ListViewRegularQueue.SelectedIndex;
                TextBoxName.Text = RegularQueue.ElementAt(index).GetName();
                TextBoxModel.Text = RegularQueue.ElementAt(index).GetModel();
                TextBoxIssue.Text = RegularQueue.ElementAt(index).GetIssue();
                TextBoxTag.Text = RegularQueue.ElementAt(index).GetTag().ToString();
                TextBoxServiceFee.Text = RegularQueue.ElementAt(index).GetCost().ToString();
                RadioButtonRegular.IsChecked = true;
                RadioButtonExpress.IsChecked = false;
                TextBoxCost.Text = RegularQueue.ElementAt(index).GetCost().ToString();
            }
        }
      
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
                TextBoxName.Text = ExpressQueue.ElementAt(index).GetName();
                TextBoxModel.Text = ExpressQueue.ElementAt(index).GetModel();
                TextBoxIssue.Text = ExpressQueue.ElementAt(index).GetIssue();
                TextBoxTag.Text = ExpressQueue.ElementAt(index).GetTag().ToString();
                TextBoxServiceFee.Text = (ExpressQueue.ElementAt(index).GetCost() / 1.15).ToString();
                RadioButtonRegular.IsChecked = false;
                RadioButtonExpress.IsChecked = true;
                TextBoxCost.Text = ExpressQueue.ElementAt(index).GetCost().ToString();
            }
        }
        private void ListViewExpressQueue_LostFocus(object sender, RoutedEventArgs e)
        {
            ListViewExpressQueue.UnselectAll();
            ClearTextBoxes();
        }
    }

}
