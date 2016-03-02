using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Pay_My_Bill.ViewModels;
using Microsoft.Phone.Scheduler;
using System.IO.IsolatedStorage;

namespace Pay_My_Bill
{
    public partial class AddAssign : PhoneApplicationPage
    {
        public AddAssign()
        {
            InitializeComponent();
        }

        private void Blah_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Blah.Text == "")
                Blah.Text = "Bill Name";
        }

        private void Blah_GotFocus(object sender, RoutedEventArgs e)
        {
            Blah.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string assignname = Blah.Text;
            DateTime due = (DateTime)datePicker.Value;

            string dueDate = due.ToString();
          //  string completed = ((ListPickerItem)CompletedProperty.SelectedItem).Content.ToString();
           // bool completedOrNot = Convert.ToBoolean(completed);
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(assignname) || assignname == "" || assignname == "Bill Name")
            {
                MessageBox.Show("Please check your bill name. Make sure there doesn't exist a bill with the same name!");
            }
            else
            {

                settings.Add(assignname, new ItemViewModel() { ID = "6", Title = assignname, DueDate = dueDate });
                settings.Save();
                
                    Reminder reminder = new Reminder(assignname);
                    reminder.Title = assignname;
                    reminder.Content = "You have your " + assignname + " bill due";
                    if (due.Date.AddDays(-2).AddHours(12) > DateTime.Now)
                        reminder.BeginTime = due.Date.AddDays(-2).AddHours(12);
                    else
                        reminder.BeginTime = due.Date.AddMonths(1).AddDays(-2).AddHours(12);
                    
                    reminder.RecurrenceType = RecurrenceInterval.Monthly;
                    reminder.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);
                    ScheduledActionService.Add(reminder);

                    NavigationService.GoBack();
            }

            //App.ViewModel.Items.Add(new ItemViewModel() { ID = "6", Title = assignname, Completed = completed, DueDate = dueDate });

        }
    }
}