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
    class StarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            switch (status)
            {
                case 1: return "../../Resources/Image/onestar.png";
                case 2: return "../../Resources/Image/twostar.png";
                case 3: return "../../Resources/Image/threestar.png";
                case 4: return "../../Resources/Image/fourstar.png";
                default: return "../../Resources/Image/fivestar.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
