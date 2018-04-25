using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Track-klasse som holder styr på et bestemt fly
    public class Track : ITrack
    {

        //Her initieres alle elementerne som et fly-track består af
        private string _Tag { get; set; }
        private Position _CurrentPosition { get; set; }
        private Position _OldPosition { get; set; }
        private Speed _CurrentSpeed { get; set; }
        private Course _CurrentCourse { get; set; }
        private Time _CurrentTime { get; set; }
        private Time _OldTime { get; set; }
        private bool _Crashing { get; set; }

        //I denne region er der lavet get- og set-metoder til alle de private members
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

        public bool Crashing
        {
            get => _Crashing;
            set => _Crashing = value;
        }
        #endregion

        //Track-constructor
        public Track(string _tag, Position _newPos, Time timestamp)
        {
            //Alle members sættes
            CurrentPosition = new Position();
            OldPosition = new Position();
            CurrentSpeed = new Speed();
            CurrentCourse = new Course();

            Tag = _tag;

            CurrentPosition.X = _newPos.X;
            CurrentPosition.Y = _newPos.Y;
            CurrentPosition.Altitude = _newPos.Altitude;

            CurrentTime = new Time("00000000000000000");
            OldTime = new Time("00000000000000000");
            CurrentTime = timestamp;

        }

        //En metode til at opdatere et tracks members
        public void UpdateTrack(string _tag, Position _newPos, Time timestamp)
        {
            //Her anvendes en hjælpefunktion til at sætte "Old"-members
            PutIntoOld(CurrentPosition, CurrentTime);

            CurrentPosition.X = _newPos.X;
            CurrentPosition.Y = _newPos.Y;
            CurrentPosition.Altitude = _newPos.Altitude;

            CurrentTime = timestamp;

            //Hastighed og kurs udregnes
            CurrentSpeed.CalculateSpeed(CurrentPosition, OldPosition, CurrentTime, OldTime);
            CurrentCourse.CalculateCourse(CurrentPosition, OldPosition);
        }

        //Hjælpefunktion der sætte current time til og position til old, således at Track kan opdateres
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

        //Udskriver et track
        public void PrintTrack()
        {
            Console.WriteLine($"Tag: {Tag} \r\nPosition (X/Y): {CurrentPosition.X} m / {CurrentPosition.Y} m\r\nAltitude: {CurrentPosition.Altitude}\r\nVelocity: {CurrentSpeed._speed} m/s\r\nCourse: {CurrentCourse._course} degrees\r\n\r\n");
        }

    }
}