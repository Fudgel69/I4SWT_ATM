using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class Speed
    {
        //Flyets hastighed i m/s
        public double _speed;
        //Udregner flyets hastighed i m/s
        public void CalculateSpeed(Position CurrentPosition, Position OldPosition, Time CurrentTime, Time OldTime)
        {

            var distance = Math.Sqrt(Math.Pow(CurrentPosition.X - OldPosition.X, 2) + Math.Pow(CurrentPosition.Y - OldPosition.Y, 2) + Math.Pow(CurrentPosition.Altitude - OldPosition.Altitude, 2));
            var timeDiffInSec = CurrentTime.TimeDifferenceSec(OldTime);

            _speed = Math.Round(distance / timeDiffInSec, 2);

        }
    }
}
