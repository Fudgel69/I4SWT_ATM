using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT4_TrackToSpeed
    {
        private Position _newPos = new Position();
        private Position _oldPos = new Position();
        private Time _oldtime;
        private Time _newtime;
        private ISpeed _speed;
        private ITrack _track;

        [SetUp]
        public void SetUp()
        {
            _newPos.SetPosition(56000, 20000, 10000);
            _oldPos.SetPosition(20000, 20000, 10000);
            _newtime = new Time("20181004095100000");
            _oldtime = new Time("20181004085100000");
            _track = new Track("Flight", _oldPos, _oldtime);

        }

        //Tester at hastigheden bliver sat korrekt
        [Test]
        public void TestSpeedCorrect()
        {
            double expectedSpeed = 10.00;
            _track.UpdateTrack("Flight", _newPos, _newtime);
            Assert.That(_track.CurrentSpeed._speed, Is.EqualTo(expectedSpeed));
        }

    }
}
