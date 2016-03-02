using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Pay_My_Bill
{
    public partial class Support : PhoneApplicationPage
    {
        public Support()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask namewhatevz = new WebBrowserTask();
            namewhatevz.Uri = new Uri("https://www.paypal.com/webapps/mpp/send-money-online", UriKind.Absolute);
            namewhatevz.Show();
        }
    }
}