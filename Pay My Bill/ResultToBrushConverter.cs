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

            
            if(threshold.IndexOf('/')==1){
            threshold = threshold.Remove(0,2);
                threshold=threshold.Remove(threshold.IndexOf('/',1));
                
            }
            else {
                threshold = threshold.Remove(0, 3);
                threshold = threshold.Remove(threshold.IndexOf('/', 1));
            }
            int date = Int16.Parse(threshold);

            if (date == 1)
                threshold = threshold + "st";
            else if(date==2)
                threshold = threshold + "nd";
            else if(date==3)
                threshold = threshold + "rd";
            else if(date==21)
                threshold = threshold + "st";
            else if (date == 22)
                threshold = threshold + "nd";
            else  if (date == 23)
                threshold = threshold + "rd";
            else
                threshold = threshold + "th";

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
