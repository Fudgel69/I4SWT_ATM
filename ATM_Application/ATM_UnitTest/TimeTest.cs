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


        private Time t = new Time("20181004085100000");
        private Time _time = new Time("20181004095100000");

        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void TestTime()
        {
            Assert.That(_time.TimeDifferenceSec(t), Is.EqualTo(3600));
        }
    }
}