using System;
using System.Globalization;
using System.Windows.Data;
using TouristAgency.Util;

namespace TouristAgency.Converter
{
    public class IsSeenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSeen = (bool)value;
            if(isSeen == false)
            {
                return "Visible";
            }
            else
            {
                return "Hidden";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
