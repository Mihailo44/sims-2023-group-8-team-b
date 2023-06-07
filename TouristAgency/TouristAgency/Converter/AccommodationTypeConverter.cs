using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TouristAgency.Util;

namespace TouristAgency.Converter
{
    public class AccommodationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TYPE type = Enum.Parse<TYPE>(value.ToString());
            switch (type)
            {
                case TYPE.HOTEL: return "HOTEL";
                case TYPE.APARTMENT: return "APARTMENT";
                case TYPE.HUT: return "HUT";
                default: return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;

            switch (stringValue)
            {
                case "HOTEL": return TYPE.HOTEL ;
                case "APARTMENT": return TYPE.APARTMENT;
                case "HUT": return TYPE.HUT;
                default: return TYPE.HOTEL;
            }
        }
    }
}
