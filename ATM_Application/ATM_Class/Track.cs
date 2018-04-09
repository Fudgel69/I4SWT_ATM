using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class Track : ITrack
    {
        private string _Tag { get; set; }
        private Position _CurrentPosition { get; set; }
        private Position _OldPosition { get; set; }
        private Speed _CurrentSpeed { get; set; }
        private Course _CurrentCourse { get; set; }
        private Time _CurrentTime { get; set; }
        private Time _OldTime { get; set; }


        #region Get/Set metoder

        public string Tag
        {
            get => _Tag;
            set => _Tag = value;
        }

        public Position CurrentPosition
        {
            get => _CurrentPosition;
            set => _CurrentPosition = value;
        }

        public Position OldPosition
        {
            get => _OldPosition;
            set => _OldPosition = value;

        }
        public Speed CurrentSpeed
        {
            get => _CurrentSpeed;
            set => _CurrentSpeed = value;
        }

        public Course CurrentCourse
        {
            get => _CurrentCourse;
            set => _CurrentCourse = value;
        }

        public Time CurrentTime
        {
            get => _CurrentTime;
            set => _CurrentTime = value;
        }

        public Time OldTime
        {
            get => _OldTime;
            set => _OldTime = value;
        }
        #endregion


        public Track(string _tag, int x, int y, int _altitude, Time timestamp)
        {
            Tag = _tag;

            CurrentPosition = new Position();
            OldPosition = new Position();
            CurrentSpeed = new Speed();
            CurrentCourse = new Course();

            CurrentPosition.X = x;
            CurrentPosition.Y = y;
            CurrentPosition.Altitude = _altitude;

            CurrentTime = new Time("00000000000000000");
            OldTime = new Time("00000000000000000");
            CurrentTime = timestamp;

        }

        public void UpdateTrack(string tag, int x, int y, int z, Time _time)
        {
            PutIntoOld(CurrentPosition, CurrentTime);
            CurrentPosition.X = x;
            CurrentPosition.Y = y;
            CurrentPosition.Altitude = z;
            CurrentTime = _time;
            CurrentSpeed.CalculateSpeed(CurrentPosition, OldPosition, CurrentTime, OldTime);
            CurrentCourse.CalculateCourse(CurrentPosition, OldPosition);
        }


        private void PutIntoOld(Position pos, Time time)
        {
            OldPosition.Y = pos.Y;
            OldPosition.X = pos.X;
            OldPosition.Altitude = pos.Altitude;
            OldTime.Day = time.Day;
            OldTime.Hour = time.Hour;
            OldTime.MilliSecond = time.MilliSecond;
            OldTime.Minute = time.Minute;
            OldTime.Month = time.Month;
            OldTime.Second = time.Second;
            OldTime.Year = time.Year;
        }

        public void PrintTrack()
        {
            Console.WriteLine($"Tag: {Tag} \r\nPosition (X/Y): {CurrentPosition.X}m/{CurrentPosition.Y}m\r\nAltitude: {CurrentPosition.Altitude}\r\nVelocity: {CurrentSpeed._speed}m/s\r\nCourse: {CurrentCourse._course}degrees\r\n\r\n");
        }


    }
}