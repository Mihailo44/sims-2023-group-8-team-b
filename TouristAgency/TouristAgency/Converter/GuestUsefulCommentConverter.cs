using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TouristAgency.Converter
{
    public class GuestUsefulCommentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool useful = (bool)value;
            if (useful == true)
            {
                return "../../../Resources/Image/star-guest.png";
            }
            else
            {
                return "../../../Resources/Image/blank.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
