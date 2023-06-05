using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using TouristAgency.Util;

namespace TouristAgency.Converter
{
    public class ComplexTourRequestStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ComplexTourRequestStatus status = (ComplexTourRequestStatus)value;
            switch (status)
            {
                case ComplexTourRequestStatus.PENDING: return "../../../Resources/Image/pending.png";
                case ComplexTourRequestStatus.INVALID: return "../../../Resources/Image/denied.png";
                default: return "../../../Resources/Image/check.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
