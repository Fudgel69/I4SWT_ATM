using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class Time
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int MilliSecond { get; set; }

        public Time(string timestamp)
        {
            Year = int.Parse(timestamp.Substring(0, 4));
            Month = int.Parse(timestamp.Substring(4, 2));
            Day = int.Parse(timestamp.Substring(6, 2));
            Hour = int.Parse(timestamp.Substring(8, 2));
            Minute = int.Parse(timestamp.Substring(10, 2));
            Second = int.Parse(timestamp.Substring(12, 2));
            MilliSecond = int.Parse(timestamp.Substring(14, 3));
        }

        public double CalculateTimeDiffrenceinSeconds(Time time)
        {
            double result = (Day - time.Day) * 24 * 60 * 60;
            result += (Hour - time.Hour) * 60 * 60;
            result += (Minute - time.Minute) * 60;
            result += (Second - time.Second);
            result += (MilliSecond - time.MilliSecond) / 1000;

            return result;
        }
    }
}
