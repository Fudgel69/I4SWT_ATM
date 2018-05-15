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
        public void TestConstructor()
        {
            Assert.That(_time.Year, Is.EqualTo(2018));
            Assert.That(_time.Month, Is.EqualTo(10));
            Assert.That(_time.Day, Is.EqualTo(04));
            Assert.That(_time.Hour, Is.EqualTo(09));
            Assert.That(_time.Minute, Is.EqualTo(51));
            Assert.That(_time.Second, Is.EqualTo(00));
            Assert.That(_time.MilliSecond, Is.EqualTo(000));
        }
        [Test]
        public void TestTime()
        {
            Assert.That(_time.TimeDifferenceSec(t), Is.EqualTo(3600));
        }
    }
}