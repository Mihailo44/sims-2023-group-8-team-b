using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TouristAgency.Model;

namespace TouristAgency.View.ValueConverter
{
    class StringToAddress : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] data = ((string)value).Split(", ");
            string country = "";
            string city = "";
            string streetName = "";
            string streetNum = "";
            if (data.Length == 2)
            {
                country = data[0];
                city = data[1];
            }
            else if(data.Length == 4)
            {
                country = data[0];
                city = data[1];
                streetName = data[2];
                streetNum = data[3];
            }
            return new Location(streetName, streetNum, city, country);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Location location = (Location)value;
            if (location.Street != "" && location.StreetNumber != "")
            {
                return location.Country + ", " + location.City + ", " + location.Street + ", " + location.StreetNumber;
            }
            else
            {
                return location.Country + ", " + location.City;
            }
        }
    }
}
