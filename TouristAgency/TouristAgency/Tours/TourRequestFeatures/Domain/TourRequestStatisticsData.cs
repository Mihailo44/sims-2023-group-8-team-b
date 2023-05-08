using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Statistics
{
    public class TourRequestStatisticsData
    {
        private string _title;
        private int _value;

        public TourRequestStatisticsData()
        {
            _title = "";
            _value = 0;
        }

        public TourRequestStatisticsData(string title, int value)
        {
            _title = title;
            _value = value;
        }

        public string Title
        {
            get => _title; 
            set 
            { 
                if (value != _title)
                {
                    _title = value;
                }
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if ( value != _value)
                {
                    _value = value;
                }
            }
        }
    }
}
