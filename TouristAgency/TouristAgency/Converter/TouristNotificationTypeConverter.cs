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
    public class TouristNotificationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TouristNotificationType type = (TouristNotificationType)value;
            switch (type)
            {
                case TouristNotificationType.SUGGESTED_TOUR_LOCATION: return "../../../Resources/Image/gps1.png";
                case TouristNotificationType.SUGGESTED_TOUR_LANGUAGE: return "../../../Resources/Image/language1.png";
                case TouristNotificationType.TOUR_REQUEST_ACCEPTED: return "../../../Resources/Image/accepted.png";
                //case TouristNotificationType.CANCELED_TOUR: return "../../../Resources/Image/gps.png";
                //case TouristNotificationType.VOUCHER: return "../../../Resources/Image/voucher.png";
                default: return "../../../Resources/Image/voucher.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
