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
using Pay_My_Bill.ViewModels;
using Microsoft.Phone.Scheduler;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Animation;

namespace Pay_My_Bill
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            IsolatedStorageSettings settings1 = IsolatedStorageSettings.ApplicationSettings;

            if (settings1.Contains("ratecount") && !settings1.Contains("reviewed"))
            {
                int count = Convert.ToInt32(settings1["ratecount"]);
                if (count != -1)
                    count++;
                if (count % 5 == 0 && count != -1)
                {
                    //Add Dialog Code Here
                    // MessageBoxButton btn = new MessageBoxButton();

                    MessageBoxResult result = MessageBox.Show("Please take a moment to review this application. It means a lot to us :)", "Would you like to rate this application?", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

                        marketplaceReviewTask.Show();
                        settings1.Add("reviewed", true);
                        settings1.Save();
                    }


                }
                if (count == 5)
                    count = 0;

                settings1["ratecount"] = count;
                settings1.Save();
            }
            else
            {
                if (!settings1.Contains("ratecount"))
                    settings1.Add("ratecount", 0);
                settings1.Save();
            }
            //MainLongListSelector.ItemsSource = App.ViewModel.Items;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            else
            {
                //InitializeComponent();                
                App.ViewModel.Items.Clear();
                App.ViewModel.LoadData();


                //MainLongListSelector.ItemsSource = null;
                //MainLongListSelector.ItemsSource = App.ViewModel.Items;



            }
            if (App.ViewModel.Items.Count > 6)
            {
                ApplicationBar.Opacity = 1;
            }

        }

        // Handle selection changed on LongListSelector
        //private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // If selected item is null (no selection) do nothing
        //    //if (MainLongListSelector.SelectedItem == null)
        //    //    return;

        //    //// Navigate to the new page
        //    //NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

        //    //// Reset selected item to null (no selection)
        //    //MainLongListSelector.SelectedItem = null;
        //}



        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            var control = (sender as StackPanel);
            TextBlock control1 = VisualTreeHelper.GetChild(control, 0) as TextBlock;
            string name = control1.Text;
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + name, UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddAssign.xaml", UriKind.Relative));
        }

        private void TextBlock_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var textbox = (sender as TextBlock).Parent;
            Image img = VisualTreeHelper.GetChild(textbox, 0) as Image;
            img.Visibility = Visibility.Visible;

        }

        private void OnFlick(object sender, FlickGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal)
            {
                DoubleAnimation db = new DoubleAnimation();
                if (e.HorizontalVelocity < 0)
                {
                   
                   // MessageBox.Show("Left");
                    var something = (sender as StackPanel);

                    // var something1 = something.SelectedItem as DependencyObject;
                    Image something2 = VisualTreeHelper.GetChild(something, 0) as Image;

                    if (something2.Width > 0) { 
                   
                    db.From = 50;
                    db.To = 0;
                    db.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                    Storyboard.SetTarget(db, something2);
                    Storyboard.SetTargetProperty(db, new PropertyPath(Image.WidthProperty));
                    Storyboard st = new Storyboard();
                    st.Children.Add(db);
                    st.Begin();
                    }
                }
                else
                {
                    var something = (sender as StackPanel);
                   
                   // var something1 = something.SelectedItem as DependencyObject;
                   
                    Image something2 = VisualTreeHelper.GetChild(something, 0) as Image;

                    if (something2.Width < 50)
                    {
                        
                        db.From = 0;
                        db.To = 50;
                        db.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                        Storyboard.SetTarget(db, something2);
                        Storyboard.SetTargetProperty(db, new PropertyPath(Image.WidthProperty));
                        Storyboard st = new Storyboard();
                        st.Children.Add(db);
                        st.Begin();
                    }
                    
                   // img.Visibility = Visibility.Visible;
                }
            }
           
        }

        private void DeleteButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image sendElement = (sender as Image);
            sendElement.Visibility = Visibility.Collapsed;
            var sp = sendElement.Parent;
            StackPanel sp1 = VisualTreeHelper.GetChild(sp, 1) as StackPanel;
            TextBlock tp = VisualTreeHelper.GetChild(sp1, 0) as TextBlock;
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(tp.Text))
                settings.Remove(tp.Text);
            settings.Save();
            if (ScheduledActionService.Find(tp.Text) != null)
                ScheduledActionService.Remove(tp.Text);
            App.ViewModel.Items.Clear();
            App.ViewModel.LoadData();


        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Support.xaml", UriKind.Relative));
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