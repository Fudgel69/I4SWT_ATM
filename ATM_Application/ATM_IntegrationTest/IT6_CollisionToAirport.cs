using System.Collections.Generic;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT6_CollisionToAirport
    {

        private Position _pos = new Position();
        private Time _time;
        private ISpeed _speed;
        private Track _trackOne;
        private Track _trackTwo;
        private Track _trackThree;
        private ITransponderReceiver _receiver;
        private INotify _collisionDetector;
        private AirspaceMonitor _airspaceMonitor;

        private RawTransponderDataEventArgs _eventArgs_1;
        private RawTransponderDataEventArgs _eventArgs_2;
        private RawTransponderDataEventArgs _eventArgs_3;

        [SetUp]
        public void SetUp()
        {

            _pos.SetPosition(39045, 12932, 14000);
            _time = new Time("20151006213456789");
            _trackOne = new Track("ABC123", _pos, _time);
            _trackTwo = new Track("XYZ789", _pos, _time);
            _trackThree = new Track("Hejsa", _pos, _time);

            _receiver = Substitute.For<ITransponderReceiver>();
            _airspaceMonitor = new AirspaceMonitor(_receiver);

            _eventArgs_1 = new RawTransponderDataEventArgs(new List<string>() { "ABC123;39045;12933;14003;20151006213456789" });
            _eventArgs_2 = new RawTransponderDataEventArgs(new List<string>() { "XYZ789;39044;12932;14002;20151006213456789" });
            _eventArgs_3 = new RawTransponderDataEventArgs(new List<string>() { "Hejsa;39043;12931;14001;20151006213456789" });



        }

        [Test]
        public void NoCollisionOnePlane()
        {
            bool raised = false;
            // Insert values equal to _plane_1
            _receiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_1);

            Raise.EventWith(new object(), _eventArgs_1);
            _airspaceMonitor.CrashTester.CrashingEvent += (sender, args) => raised = true;

            _airspaceMonitor.CrashTester.Update(_airspaceMonitor.Tracks);

            Assert.That(raised == false);
        }

        [Test]
        public void CollisionTwoPlanes()
        {
            bool raised = false;

            _airspaceMonitor.CrashTester.CrashingEvent += (sender, args) => raised = true;
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_1);
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_2);

            Assert.That(raised);
        }

        [Test]
        public void CollisionThreePlanes()
        {
            int raised = 0;
            bool rased = false;

            _airspaceMonitor.CrashTester.CrashingEvent += (sender, args) => raised += 1;
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_1);
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_2);
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_3);

            Assert.That(raised, Is.EqualTo(3));
        }

        [Test]
        public void RemoveOneCollision()
        {
            RawTransponderDataEventArgs _eventArgs_4 =
                new RawTransponderDataEventArgs(new List<string>()
                { "ABC123;30045;12933;14003;20151006213457789" });

            bool raised = false;

            _airspaceMonitor.CrashTester.CrashingEvent += (sender, args) => raised = true;

            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_1);
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_2);


            // Change coords of _plane_1
            _airspaceMonitor._TransponderReceiver.TransponderDataReady += Raise.EventWith(new object(), _eventArgs_4);

            Assert.That(raised);
        }



    }
}