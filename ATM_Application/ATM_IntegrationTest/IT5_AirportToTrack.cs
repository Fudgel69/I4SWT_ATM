using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM_IntegrationTest
{
    public class IT5_AirportToTrack
    {

        [TestFixture]
        public class IT4_TrackToSpeed
        {
            private Position _newPos = new Position();
            private Position _oldPos = new Position();
            private Time _timeOne;
            private Time _timeTwo;
            private ISpeed _speed;
            private ITrack _trackOne;
            private ITrack _trackTwo;
            private ITransponderReceiver _receiver;
            private AirspaceMonitor _airspaceMonitor;

            [SetUp]
            public void SetUp()
            {
                _newPos.SetPosition(56000, 20000, 10000);
                _oldPos.SetPosition(20000, 20000, 10000);
                _timeOne = new Time("20181004095100000");
                _timeTwo = new Time("20181004085100000");
                _trackOne = new Track("Flight", _oldPos, _timeTwo);

                _receiver = Substitute.For<ITransponderReceiver>();
                _airspaceMonitor = new AirspaceMonitor(_receiver);

            }



        }

    }
}