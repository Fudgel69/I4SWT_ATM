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
        private Speed _speed;
        
        private Position CurPos;
        private Position OldPos;
        private Time CurTime;
        private Time OldTime;
        

        [SetUp]
        public void Setup()
        {
            _speed.CalculateSpeed(CurPos, OldPos, CurTime, OldTime);
        }


        [Test]
        public void TestCalculateSpeed()
        {

        }


    }
}
