using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using ATM_Class.Classes;
using NSubstitute;
using NUnit.Framework;

namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT1_TrackToPosition
    {
        private Position _position;
        private Time _time;
        private ITrack _track;

        [SetUp]
        public void SetUp()
        {
            _position = new Position
            {
                X = 20000,
                Y = 20000,
                Altitude = 5000
            };
            _time = Substitute.For<Time>();
            _track = new Track("Dillerdallerfly", _position, _time);
        }

        [Test]
        public void CurrentPositionIsCorrectlyParsed_WhenUpdatingTrack()
        {
            Assert.That(_track.CurrentPosition, Is.EqualTo(_position));
        }

    }
}
