﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TouristAgency.Converter
{
    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int d = (int)value;
            return string.Format("{0}", d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            int intValue;

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return 1;
            }
            else
            {
                if (int.TryParse(stringValue, out intValue))
                {
                    return intValue;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
