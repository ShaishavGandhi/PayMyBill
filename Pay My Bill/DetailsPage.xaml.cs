using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Pay_My_Bill.Resources;
using Microsoft.Phone.Scheduler;
using Pay_My_Bill.ViewModels;
using System.IO.IsolatedStorage;

namespace Pay_My_Bill
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        public int count = 0;
        public string assignmentName;
        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                //string selectedIndex = "";
                //if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                //{
                //    int index = int.Parse(selectedIndex);
                //    DataContext = App.ViewModel.Items[index];
                //}
                if (NavigationContext.QueryString.ContainsKey("selectedItem"))
                {

                    assignmentName = NavigationContext.QueryString["selectedItem"];
                    Blah.Text = assignmentName;
                    count++;
                    // etc ...
                }
                //for (int i = 0; i < App.ViewModel.Items.Count; i++)
                //{
                //    if (App.ViewModel.Items[i].Title == assignmentName)
                //    {
                //DateTime date = new DateTime(App.ViewModel.Items[i].DueDate.ToString());
                // var assignDate = (.ToString() as DateTime);
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(assignmentName))
                {
                    // if(datePicker.Value==DateTime.Now)
                    if (count == 1)
                        datePicker.Value = Convert.ToDateTime((settings[assignmentName] as ItemViewModel).DueDate);
                   // Option1.Content = (settings[assignmentName] as ItemViewModel).Completed;
                    //string blah = Option1.Content.ToString();
                   
                }
                //    }

                //}

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string reference = Blah.Text;
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(reference))
            {
                settings.Remove(reference);
                settings.Save();
                if (ScheduledActionService.Find(assignmentName) != null)
                    ScheduledActionService.Remove(assignmentName);
                NavigationService.GoBack();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = Blah.Text;
            DateTime due = (DateTime)datePicker.Value;
            string duedate = due.ToString();
           

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(name))
            {
                settings[name] = new ItemViewModel() { ID = "8", Title = name, DueDate = duedate };
                if (ScheduledActionService.Find(name) != null)
                    ScheduledActionService.Remove(name);
                
                    Reminder reminder = new Reminder(name);
                    reminder.Title = name;
                    reminder.Content = "You have your " + name + " bill due in two days";
                    if (due.Date.AddDays(-2).AddHours(12) > DateTime.Now)
                        reminder.BeginTime = due.Date.AddDays(-2).AddHours(12);
                    else
                        reminder.BeginTime = due.Date.AddMonths(1).AddDays(-2).AddHours(12);
                    reminder.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);
                    reminder.RecurrenceType = RecurrenceInterval.Monthly;
                    ScheduledActionService.Add(reminder);

                
                settings.Save();
                NavigationService.GoBack();
            }


        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}