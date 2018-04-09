using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class Course
    {
        //Flyets kurs i grader
        public double _course;

        //Udregner flyets kurs i grader
        public void CalculateCourse(Position CurrentPosition, Position OldPosition)
        {
            if (OldPosition.X == CurrentPosition.X && OldPosition.Y == CurrentPosition.Y)
            {
                _course = double.NaN;
                return;
            }

            var x = (Math.Atan2(CurrentPosition.Y - OldPosition.Y, CurrentPosition.X - OldPosition.X) * 180 / Math.PI);

            var temp = Math.Round(x - 90, 2);

            if (temp > 0)
            {
                _course = 360 - temp;
            }
            else
            {
                _course = Math.Abs(temp);
            }
        }
    }
}
