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

        private Track _plane_1;
        private Track _plane_2;
        private Track _plane_3;

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

    }
}