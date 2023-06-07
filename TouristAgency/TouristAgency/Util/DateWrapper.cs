using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Util
{
    public class DateWrapper
    {
        DateTime _date;
        DateTime _timestamp;
        public DateWrapper(DateTime date)
        {
            _date = date;
            _timestamp = DateTime.Now;
        }
        public DateTime Date
        {
            get { return _date; } 
        }
        public DateTime Timestamp
        {
            get { return _timestamp; }
        }
    }
}
