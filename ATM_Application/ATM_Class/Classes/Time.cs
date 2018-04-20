using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Time-klasse som består af tidsenheder fra år til millisekunder
    public class Time : ITime
    {
        //Alle private members (tidsenheder fra år til millisekunder)
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int MilliSecond { get; set; }

        //Constructor for Time, her parses en string og udfra denne bliver de tilsvarende private members sat
        public Time()
        {

        }
        public Time(string t)
        {
            Year = int.Parse(t.Substring(0, 4));
            Month = int.Parse(t.Substring(4, 2));
            Day = int.Parse(t.Substring(6, 2));
            Hour = int.Parse(t.Substring(8, 2));
            Minute = int.Parse(t.Substring(10, 2));
            Second = int.Parse(t.Substring(12, 2));
            MilliSecond = int.Parse(t.Substring(14, 3));
        }

        //Funktion til udregning af tidsforskel på nuværende og parset tid i sekunder
        public double TimeDifferenceSec(Time t)
        {
            double result = (Day - t.Day) * 24 * 60 * 60;
            result += (Hour - t.Hour) * 60 * 60;
            result += (Minute - t.Minute) * 60;
            result += (Second - t.Second);
            result += (MilliSecond - t.MilliSecond) / 1000;

            return result;
        }
    }
}
