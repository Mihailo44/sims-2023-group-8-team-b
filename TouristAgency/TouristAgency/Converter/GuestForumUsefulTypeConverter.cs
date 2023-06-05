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
    public class GuestForumUsefulTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool useful = (bool)value;
            if(useful == true)
            {
                return "../../../Resources/Image/checked2-guest.png";
            }
            else
            {
                return "../../../Resources/Image/cancel-guest.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
