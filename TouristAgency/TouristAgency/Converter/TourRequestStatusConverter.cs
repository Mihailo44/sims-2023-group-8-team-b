using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TouristAgency.TourRequests;
using TouristAgency.Util;

namespace TouristAgency.Converter
{
    public class TourRequestStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TourRequestStatus status = (TourRequestStatus)value;
            switch(status)
            {
                case TourRequestStatus.PENDING: return "../../../Resources/Image/pending.png";
                case TourRequestStatus.INVALID: return "../../../Resources/Image/denied.png";
                default : return "../../../Resources/Image/check.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
