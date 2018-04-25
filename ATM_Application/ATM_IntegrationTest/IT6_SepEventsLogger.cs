using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT6_SepEventsLogger
    {
        private NewSepEvent _newSepEvent;
        private SepEventsLogger _sepEventsLogger;
        private int nEventsRaised;
        private Position _pos;
        private Time _time;
        private ITrack _trackOne;
        private ITrack _trackTwo;



        [SetUp]
        public void SetUp()
        {
            _pos.SetPosition(10000, 10000, 10000);
            _time = new Time("20181010105111111");
            _trackOne = new Track("ABC123", _pos, _time);
            _trackTwo = new Track("XYZ789", _pos, _time);
            _newSepEvent = new NewSepEvent();
            _newSepEvent.CrashingEvent += delegate { nEventsRaised++; };
            nEventsRaised = 0;
            _sepEventsLogger = new SepEventsLogger();
        }

        //Tester at hvis positionen bliver lavet om, vil der også blive lavet en kurs
        [Test]
        public void TestCrashingEventInvoked()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "SepEventsLog.txt";
            var TRACK = new List<ITrack>();
                TRACK.Add(_trackOne);
                TRACK.Add(_trackTwo);
                //$"{_trackOne.Tag};{_trackOne.CurrentPosition.X};{_trackOne.CurrentPosition.Y};{_trackOne.CurrentPosition.Altitude};{_trackOne.CurrentTime.Year}{_trackOne.CurrentTime.Month}{_trackOne.CurrentTime.Day}{_trackOne.CurrentTime.Hour}{_trackOne.CurrentTime.Minute}{_trackOne.CurrentTime.Second}{_trackOne.CurrentTime.MilliSecond}"

            _newSepEvent.Update(TRACK);

            //Assert.That(nEventsRaised, Is.EqualTo(1));

            Assert.IsTrue(File.Exists(path));
        }

    }
}