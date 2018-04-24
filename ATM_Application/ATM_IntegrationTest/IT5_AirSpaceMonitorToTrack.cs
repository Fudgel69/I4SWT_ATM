using System.Collections.Generic;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM_IntegrationTest
{

    [TestFixture]
    public class IT5_AirSpaceMonitorToTrack
    {



        private Position _pos = new Position();
        private Time _time;
        private ISpeed _speed;
        private Track _trackOne;
        private Track _trackTwo;
        private ITransponderReceiver _receiver;
        private AirspaceMonitor _airspaceMonitor;

        [SetUp]
        public void SetUp()
        {
            _pos.SetPosition(20000, 20000, 10000);
            _time = new Time("20181010105111111");
            _trackOne = new Track("ABC123", _pos, _time);
            _trackTwo = new Track("XYZ789", _pos, _time);

            _receiver = Substitute.For<ITransponderReceiver>();
            _airspaceMonitor = new AirspaceMonitor(_receiver);

        }

        //Tester hvorom et nyt fly bliver tilføjet hvis et event raises med dette
        [Test]
        public void NewTrack()
        {
            var TRACK = new List<string>
                {
                    $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
                };


            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks[0].Tag, Is.EqualTo(_trackOne.Tag));
        }

        //Tester at der kan lægges to seperate tracks ind i Airports track-liste
        [Test]
        public void TwoNewTracks()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}",
                $"{_trackTwo.Tag};{_trackTwo.CurrentPosition.X};{_trackTwo.CurrentPosition.Y};{_trackTwo.CurrentPosition.Altitude};{_trackTwo.CurrentTime.Year}{_trackTwo.CurrentTime.Month}{_trackTwo.CurrentTime.Day}{_trackTwo.CurrentTime.Hour}{_trackTwo.CurrentTime.Minute}{_trackTwo.CurrentTime.Second}{_trackTwo.CurrentTime.MilliSecond}"
            };


            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks.Count, Is.EqualTo(2));
            Assert.That(_airspaceMonitor.Tracks[0].Tag, Is.EqualTo(_trackOne.Tag));
            Assert.That(_airspaceMonitor.Tracks[1].Tag, Is.EqualTo(_trackTwo.Tag));
        }

        //Tester om et fly bliver opdateret når et event raises med dette
        [Test]
        public void UpdateTrack()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            _pos.SetPosition(10000, 10000, 10000);
            _trackOne.UpdateTrack("ABC123", _pos, _time);
            TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks[0].CurrentPosition.X, Is.EqualTo(_trackOne.CurrentPosition.X));
            Assert.That(_airspaceMonitor.Tracks[0].CurrentPosition.Y, Is.EqualTo(_trackOne.CurrentPosition.Y));
            Assert.That(_airspaceMonitor.Tracks[0].CurrentPosition.Altitude, Is.EqualTo(_trackOne.CurrentPosition.Altitude));
        }

        //Tester om en opdatering af et fly vil give den en kurs
        [Test]
        public void UpdateTrackMakesCourse()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            _pos.SetPosition(10000, 10000, 10000);
            _trackOne.UpdateTrack("ABC123", _pos, _time);
            TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks[0].CurrentCourse._course, Is.EqualTo(_trackOne.CurrentCourse._course));
        }

        //Tester om en opdatering af et fly vil give den en hastighed
        [Test]
        public void UpdateTrackMakesSpeed()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));
            _time = new Time("20181010115111111");
            _pos.SetPosition(10000, 10000, 10000);
            _trackOne.UpdateTrack("ABC123", _pos, _time);
            
            TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks[0].CurrentSpeed._speed, Is.EqualTo(_trackOne.CurrentSpeed._speed));
        }

    }
}