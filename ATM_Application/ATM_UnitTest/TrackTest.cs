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
        private int _x = 20000;
        private int _y = 20000;
        private int _altitude = 10000;
        private Time _timeStamp = new Time("20181004095400000");

        private Track _track;

        private List<string> _list;

        [SetUp]
        public void Setup()
        {
            _track = new Track(_tag, _x, _y, _altitude, _timeStamp);
        }

        [Test]
        public void ChangePositionSpeedChanges()
        {
            double OldSpeed = _track.CurrentSpeed._speed;
            _track.UpdateTrack(_tag, 10000, 10000, 5000, _timeStamp);
            Assert.That(OldSpeed, !Is.EqualTo(_track.CurrentSpeed._speed));
        }

        [Test]
        public void ChangePositionCourseChanges()
        {
            double OldCourse = _track.CurrentCourse._course;
            _track.UpdateTrack(_tag, 10000, 10000, 5000, _timeStamp);
            Assert.That(OldCourse, !Is.EqualTo(_track.CurrentCourse._course));
        }

        [Test]
        public void UpdateTrackChangesPositionX()
        {
            int OldX = _track.CurrentPosition.X;
            _track.UpdateTrack(_tag, 10000, 10000, 5000, _timeStamp);
            Assert.That(OldX, !Is.EqualTo(_track.CurrentPosition.X));
        }

        [Test]
        public void UpdateTrackChangesPositionY()
        {
            int OldY = _track.CurrentPosition.Y;
            _track.UpdateTrack(_tag, 10000, 10000, 5000, _timeStamp);
            Assert.That(OldY, !Is.EqualTo(_track.CurrentPosition.Y));
        }

        [Test]
        public void UpdateTrackChangesPositionAltitude()
        {
            int OldAltitude = _track.CurrentPosition.Altitude;
            _track.UpdateTrack(_tag, 10000, 10000, 5000, _timeStamp);
            Assert.That(OldAltitude, !Is.EqualTo(_track.CurrentPosition.Altitude));
        }

        [Test]
        public void UpdateTrackChangesTime()
        {
            Time OldTime = _track.CurrentTime;
            Time NewTime = new Time("20191105101512345");
            _track.UpdateTrack(_tag, 10000, 10000, 5000, NewTime);

            Assert.That(OldTime, !Is.EqualTo(_track.CurrentTime));
        }



    }
}
