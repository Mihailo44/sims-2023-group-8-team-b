using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TouristAgency.Converter
{
    public class MonthConverter
    {
        private static Dictionary<int, string> months = new Dictionary<int, string>
        {
            {0,"Nema"},
            {1,"January"},
            {2,"February"},
            {3,"March"},
            {4,"April"},
            {5,"May"},
            {6,"June"},
            {7,"July"},
            {8,"August"},
            {9,"September"},
            {10,"October"},
            {11,"November"},
            {12,"December"}
        };

        public static string GetMonthName(int id)
        {
            return months[id];
        }

        public static int GetMonthId(string value)
        {
            foreach(var month in months)
            {
                if(month.Value == value)
                {
                    return month.Key;
                }
            }

            return -1;
        }
    }
}
