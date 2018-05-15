using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM_Class;

namespace ATM_UnitTest
{
    [TestFixture]
    class SpeedTest
    {
        private Speed _speed = new Speed();
        
        private Position CurPos = new Position();
        private Position OldPos = new Position();
        private Time CurTime = new Time("20181004095100000");
        private Time OldTime = new Time("20181004085100000");


        
        [SetUp]
        public void Setup()
        {
            CurPos.SetPosition(56000, 20000, 10000);
            OldPos.SetPosition(20000, 20000, 10000 );
        }


        [Test]
        public void TestCalculateSpeed()
        {
            double expectedSpeed = 10.00;
            _speed.CalculateSpeed(CurPos, OldPos, CurTime, OldTime);
            Assert.That(_speed._speed, Is.EqualTo(expectedSpeed));

        }


    }
}
