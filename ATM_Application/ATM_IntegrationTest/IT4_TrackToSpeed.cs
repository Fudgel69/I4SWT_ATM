using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT4_TrackToSpeed
    {
        private IPosition _newPos = new Position();
        private IPosition _oldPos = new Position();
        private ITime _oldtime;
        private ITime _newtime;
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
        [Test]
        public void TestSpeedCorrect()
        {
            double expectedSpeed = 10.00;
            _track.UpdateTrack("Flight", _newPos, _newtime);
            Assert.That(_track.CurrentSpeed, Is.EqualTo(expectedSpeed));
        }

    }
}
