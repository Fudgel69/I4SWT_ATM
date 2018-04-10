using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATM_UnitTest
{
    [TestFixture]
    class AirspaceMonitorTest
    {
        private AirspaceMonitor _airspaceMonitor;
        private ITrack _track;
        private ITransponderReceiver _receiver;

        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _track = Substitute.For<ITrack>();
            _airspaceMonitor = new AirspaceMonitor(_receiver);
        }

        [Test]
        public void NewTrackDataCreated()
        {
            var TRACK = new List<string>
            {
                "ATR423;39045;12932;14000;20151006213456789"
            };

            _track.Tag.Returns("ATR423");

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks[0].Tag, Is.EqualTo(_track.Tag));
        }

        [Test]
        public void NewTrackDataUpdate()
        {
            var TRACK = new List<string>
            {
                "ATR423;39045;12932;14000;20151006213456789"
            };

            _track.Tag.Returns("ATR423");

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks.Count, Is.EqualTo(1));
        }

        [Test]
        public void NewTrackNewCreated()
        {
            var TRACK = new List<string>
            {
                "ATR433;39045;12932;14000;20151006213456789",
                "ATR423;39045;12932;14000;20151006213456789"
            };

            _track.Tag.Returns("ATR423");

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(TRACK));

            Assert.That(_airspaceMonitor.Tracks.Count, Is.EqualTo(2));
        }

    }


    
}
