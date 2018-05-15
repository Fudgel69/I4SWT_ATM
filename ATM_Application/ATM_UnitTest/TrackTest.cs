using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM_UnitTest
{
    [TestFixture]
    class TrackTest
    {
        private string _tag = "AC420";
        private Position _pos = new Position();
        private int _x = 20000;
        private int _y = 20000;
        private int _altitude = 10000;
        private Time _timeStamp = new Time("20181004095400000");

        private Track _track;

        private List<string> _list;

        [SetUp]
        public void Setup()
        {
            _pos.SetPosition(_x, _y, _altitude);
            _track = new Track(_tag, _pos, _timeStamp);
            _track.Crashing = false;
            _track.CrashTime = new Time("20181004094400000");
        }

        [Test]
        public void ChangePositionSpeedChanges()
        {
            _pos.SetPosition(10000, 10000, 5000);
            double OldSpeed = _track.CurrentSpeed._speed;
            _track.UpdateTrack(_tag, _pos, _timeStamp);
            Assert.That(OldSpeed, !Is.EqualTo(_track.CurrentSpeed._speed));
        }

        [Test]
        public void ChangePositionCourseChanges()
        {
            _pos.SetPosition(10000, 10000, 5000);
            double OldCourse = _track.CurrentCourse._course;
            _track.UpdateTrack(_tag, _pos, _timeStamp);
            Assert.That(OldCourse, !Is.EqualTo(_track.CurrentCourse._course));
        }

        [Test]
        public void UpdateTrackChangesPositionX()
        {
            _pos.SetPosition(10000, 10000, 5000);
            int OldX = _track.CurrentPosition.X;
            _track.UpdateTrack(_tag, _pos, _timeStamp);
            Assert.That(OldX, !Is.EqualTo(_track.CurrentPosition.X));
        }

        [Test]
        public void UpdateTrackChangesPositionY()
        {
            _pos.SetPosition(10000, 10000, 5000);
            int OldY = _track.CurrentPosition.Y;
            _track.UpdateTrack(_tag, _pos, _timeStamp);
            Assert.That(OldY, !Is.EqualTo(_track.CurrentPosition.Y));
        }

        [Test]
        public void UpdateTrackChangesPositionAltitude()
        {
            _pos.SetPosition(10000, 10000, 5000);
            int OldAltitude = _track.CurrentPosition.Altitude;
            _track.UpdateTrack(_tag, _pos, _timeStamp);
            Assert.That(OldAltitude, !Is.EqualTo(_track.CurrentPosition.Altitude));
        }

        [Test]
        public void UpdateTrackChangesTime()
        {
            _pos.SetPosition(10000, 10000, 5000);
            Time OldTime = _track.CurrentTime;
            Time NewTime = new Time("20191105101512345");
            _track.UpdateTrack(_tag, _pos, NewTime);

            Assert.That(OldTime, !Is.EqualTo(_track.CurrentTime));
        }

        [Test]
        public void UpdateCrashing ()
        {
            _track.Crashing = true;
            Assert.True(_track.Crashing);
        }
        [Test]
        public void UpdateCrashTime()
        {
            _track.CrashTime = _timeStamp;
            Assert.That(_track.CrashTime, Is.EqualTo(_timeStamp)); 
        }

    }
}
