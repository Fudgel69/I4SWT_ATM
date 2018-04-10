using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;

namespace ATM_UnitTest
{
    [TestFixture]
    class TimeTest
    {
        private Time t;
        private ITime _time;
        
        [SetUp]
        public void Setup()
        {
            _time.TimeDifferenceSec(t);
        }

        [Test]
        public void TestTime()
        {

        }


    }
}
