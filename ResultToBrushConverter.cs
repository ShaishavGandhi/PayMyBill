using System;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Globalization;

namespace Pay_My_Bill
{
    public class ResultToBrushConverter : IValueConverter
    {
        // This converts the result object to the foreground.

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // Retrieve the format string and use it to format the value.
            string threshold = value.ToString();

            threshold = threshold.Remove(threshold.Length - 8);

            return threshold;
            //switch (text)
            //{
            //    //Implement your logic here
            //    case "Late":
            //        return new SolidColorBrush(Colors.Red);
            //    case "Arrived":
            //        return new SolidColorBrush(Colors.Green);
            //    case "NA":
            //        return new SolidColorBrush(Colors.Yellow);
            //    default:
            //        return new SolidColorBrush(Colors.Black);
            //}
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
