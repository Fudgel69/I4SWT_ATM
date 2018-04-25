using System.Collections.Generic;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT6_CollisionToAirspaceMonitor
    {

        private Position _pos = new Position();
        private Time _time;
        private ISpeed _speed;
        private Track _trackOne;
        private Track _trackTwo;
        private Track _trackThree;
        private ITransponderReceiver _receiver;
        private AirspaceMonitor _airspaceMonitor;


        [SetUp]
        public void SetUp()
        {

            _pos.SetPosition(10000, 10000, 10000);
            _time = new Time("20181010105111111");
            _trackOne = new Track("ABC123", _pos, _time);
            _trackTwo = new Track("XYZ789", _pos, _time);
            _trackThree = new Track("DFG567", _pos, _time);

            _receiver = Substitute.For<ITransponderReceiver>();
            _airspaceMonitor = new AirspaceMonitor(_receiver);

            


        }

        //Tester om der kommer et kollision-event kommer hvis der kun kommer ét fly
        [Test]
        public void NoCollisionOnePlane()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"
            };

            int raised = 0;

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            _airspaceMonitor.CrashTester.NotCrashingEvent += (sender, args) => raised += 1;

            _airspaceMonitor.CrashTester.Update(_airspaceMonitor.Tracks);

            Assert.That(raised, Is.EqualTo(0));
        }

        //Tester om der kommer et kollisions-event hvis to fly er ved at støde sammen
        [Test]
        public void CollisionTwoPlanes()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}",
                $"{_trackTwo.Tag};{_trackTwo.CurrentPosition.X};{_trackTwo.CurrentPosition.Y};{_trackTwo.CurrentPosition.Altitude};{_trackTwo.CurrentTime.Year}{_trackTwo.CurrentTime.Month}{_trackTwo.CurrentTime.Day}{_trackTwo.CurrentTime.Hour}{_trackTwo.CurrentTime.Minute}{_trackTwo.CurrentTime.Second}{_trackTwo.CurrentTime.MilliSecond}"

            };

            bool raised = false;

            _airspaceMonitor.CrashTester.CrashingEvent += (sender, args) => raised = true;
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(raised, Is.EqualTo(true));
        }

        //Tester om der kommer tre kollisions-events hvis tre fly er ved at støde sammen
        [Test]
        public void CollisionThreePlanes()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}",
                $"{_trackTwo.Tag};{_trackTwo.CurrentPosition.X};{_trackTwo.CurrentPosition.Y};{_trackTwo.CurrentPosition.Altitude};{_trackTwo.CurrentTime.Year}{_trackTwo.CurrentTime.Month}{_trackTwo.CurrentTime.Day}{_trackTwo.CurrentTime.Hour}{_trackTwo.CurrentTime.Minute}{_trackTwo.CurrentTime.Second}{_trackTwo.CurrentTime.MilliSecond}",
                $"{_trackThree.Tag};{_trackThree.CurrentPosition.X};{_trackThree.CurrentPosition.Y};{_trackThree.CurrentPosition.Altitude};{_trackThree.CurrentTime.Year}{_trackThree.CurrentTime.Month}{_trackThree.CurrentTime.Day}{_trackThree.CurrentTime.Hour}{_trackThree.CurrentTime.Minute}{_trackThree.CurrentTime.Second}{_trackThree.CurrentTime.MilliSecond}"
            };

            int raised = 0;

            _airspaceMonitor.CrashTester.CrashingEvent += (sender, args) => raised += 1;
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(raised, Is.EqualTo(3));
        }

        //Tester om en kollision bliver fjernet hvis et fly ikke længere er i kollision
        [Test]
        public void RemoveOneCollision()
        {
            var TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}",
                $"{_trackTwo.Tag};{_trackTwo.CurrentPosition.X};{_trackTwo.CurrentPosition.Y};{_trackTwo.CurrentPosition.Altitude};{_trackTwo.CurrentTime.Year}{_trackTwo.CurrentTime.Month}{_trackTwo.CurrentTime.Day}{_trackTwo.CurrentTime.Hour}{_trackTwo.CurrentTime.Minute}{_trackTwo.CurrentTime.Second}{_trackTwo.CurrentTime.MilliSecond}"
            };
            int raised = 0;

            _airspaceMonitor.CrashTester.NotCrashingEvent += (sender, args) => raised += 1;

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));
            TRACK = new List<string>
            {
                $"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}",
                $"{_trackTwo.Tag};{_trackTwo.CurrentPosition.X + 10000};{_trackTwo.CurrentPosition.Y + 10000};{_trackTwo.CurrentPosition.Altitude + 10000};{_trackTwo.CurrentTime.Year}{_trackTwo.CurrentTime.Month}{_trackTwo.CurrentTime.Day}{_trackTwo.CurrentTime.Hour}{_trackTwo.CurrentTime.Minute}{_trackTwo.CurrentTime.Second}{_trackTwo.CurrentTime.MilliSecond}"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(raised, Is.EqualTo(1));
        }



    }
}