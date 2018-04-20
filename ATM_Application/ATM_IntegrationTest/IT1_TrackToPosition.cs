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
        private IPosition _position;
        private ITime _time;
        private ITrack _track;

        [SetUp]
        public void SetUp()
        {
            _position = new Position();
            _time = Substitute.For<ITime>();
            _track = new Track();
        }

        [Test]
        public void 
    }
}
