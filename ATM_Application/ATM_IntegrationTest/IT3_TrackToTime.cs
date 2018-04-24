using ATM_Class;
using NSubstitute;
using NUnit.Framework;

namespace ATM_IntegrationTest
{
    public class IT3_TrackToTime
    {

        private Position _position;
        private Time _time;
        private Track _track;
        private Course _course;

        [SetUp]
        public void SetUp()
        {
            _position = new Position();
            _position.X = 20000;
            _position.Y = 20000;
            _position.Altitude = 5000;
            _course = new Course();
            _time = new Time("20181004095100000");
            _track = new Track("Flight", _position, _time);
        }

        //Tester at tiden rent faktisk bliver sat ind i flyet
        [Test]
        public void TestTimeInTrack()
        {

            Assert.That(_track.CurrentTime.Year, Is.EqualTo(_time.Year));
            Assert.That(_track.CurrentTime.Month, Is.EqualTo(_time.Month));
            Assert.That(_track.CurrentTime.Day, Is.EqualTo(_time.Day));
            Assert.That(_track.CurrentTime.Hour, Is.EqualTo(_time.Hour));
            Assert.That(_track.CurrentTime.Minute, Is.EqualTo(_time.Minute));
            Assert.That(_track.CurrentTime.Second, Is.EqualTo(_time.Second));
            Assert.That(_track.CurrentTime.MilliSecond, Is.EqualTo(_time.MilliSecond));

        }

        //Tester at UpdateTrack opdaterer tiden
        [Test]
        public void TestTimeInTrackUpdated()
        {
            Time _timetwo = new Time("20191105085100000");
            _track.UpdateTrack("Flight", _position, _timetwo);
            Assert.That(_track.CurrentTime.Year, Is.EqualTo(_timetwo.Year));
            Assert.That(_track.CurrentTime.Month, Is.EqualTo(_timetwo.Month));
            Assert.That(_track.CurrentTime.Day, Is.EqualTo(_timetwo.Day));
            Assert.That(_track.CurrentTime.Hour, Is.EqualTo(_timetwo.Hour));
            Assert.That(_track.CurrentTime.Minute, Is.EqualTo(_timetwo.Minute));
            Assert.That(_track.CurrentTime.Second, Is.EqualTo(_timetwo.Second));
            Assert.That(_track.CurrentTime.MilliSecond, Is.EqualTo(_timetwo.MilliSecond));

        }

        //Tester, at når tiden bliver opdateret, kan oldtime bruges til at udregne en tidsforskel
        [Test]
        public void TestTimeInTrackDayPassed()
        {
            Time _timetwo = new Time("20181005095100000");
            _track.UpdateTrack("Flight", _position, _timetwo);
            Assert.That(_track.CurrentTime.TimeDifferenceSec(_track.OldTime), Is.EqualTo(86400));

        }

    }
}