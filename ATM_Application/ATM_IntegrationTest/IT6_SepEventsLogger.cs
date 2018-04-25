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
        private Position _pos;
        private Time _time;
        private ITrack _trackOne;
        private ITrack _trackTwo;



        [SetUp]
        public void SetUp()
        {
            _pos = new Position();
            _pos.SetPosition(10000, 10000, 10000);
            _time = new Time("20181010105111111");
            _trackOne = new Track("ABC123", _pos, _time);
            _trackTwo = new Track("XYZ789", _pos, _time);
            _newSepEvent = new NewSepEvent();
            _sepEventsLogger = new SepEventsLogger();
        }

        //Tester at hvis positionen bliver lavet om, vil der også blive lavet en kurs
        [Test]
        public void TestCrashingEventInvoked()
        {

            int nEventsRaised = 0;

            _newSepEvent.CrashingEvent += delegate { nEventsRaised++; };
            _newSepEvent.CrashingEvent += (sender, args) => nEventsRaised += 1;

            var TRACK = new List<ITrack>
            {
                _trackOne,
                _trackTwo
            };

            _newSepEvent.Update(TRACK);

            Assert.That(nEventsRaised, Is.EqualTo(2));
        }

    }
}