using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class Track
    {

        public Position Position { get; set; }
        public Position OldPosition { get; set; }
        public double Velocity { get; set; }
        public double CurCompasCourse { get; set; }
        public Time TimeStamp { get; set; }
        public Time OldTimeStamp { get; set; }

        private string tag;
        private int x, y, altitude;
        private DateTime currentTime;

        public string Tag
        {
            get => tag;
            set => tag = value;
        }

        public int XCoord
        {
            get => x;
            set => x = value;
        }

        public int YCoord
        {
            get => y;
            set => y = value;
        }

        public int Altitude
        {
            get => altitude;
            set => altitude = value;
        }

        public DateTime CurrentTime
        {
            get => currentTime;
            set => currentTime = value;
        }

        public Track(string _tag, int _xcoord, int _ycoord, int _altitude, DateTime _currentTime)
        {
            tag = _tag;
            x = _xcoord;
            y = _ycoord;
            altitude = _altitude;
            currentTime = _currentTime;
        }

        public override string ToString()
        {
            return String.Format($"Tag: {tag} \r\nX coordinate: {x} meters \r\nY coordinate: {y} meters \r\nAltitude: {altitude} meters \r\nTimestamp: {currentTime.Day}/{currentTime.Month}/{currentTime.Year}, at {currentTime.Hour}:{currentTime.Minute}:{currentTime.Second}\r\n");
        }

        public void UpdateTrack(string tag, int x, int y, int z, TimeStamp timestamp)
        {
            PutIntoOld(Position, TimeStamp);
            Position.XKoordinate = x;
            Position.YKoordinate = y;
            Position.ZKoordinate = z;
            TimeStamp = timestamp;
            CalculateVelocity();
            CalculateCompasCourse();
        }


        private void PutIntoOld(Position pos, TimeStamp time)
        {
            OldPosition.YKoordinate = pos.YKoordinate;
            OldPosition.XKoordinate = pos.XKoordinate;
            OldPosition.ZKoordinate = pos.ZKoordinate;
            OldTimeStamp.Day = time.Day;
            OldTimeStamp.Hour = time.Hour;
            OldTimeStamp.MilliSecond = time.MilliSecond;
            OldTimeStamp.Minute = time.Minute;
            OldTimeStamp.Month = time.Month;
            OldTimeStamp.Second = time.Second;
            OldTimeStamp.Year = time.Year;
        }
    }
}