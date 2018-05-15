using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM_Class;
using NSubstitute;
using TransponderReceiver;

namespace ATM_UnitTest
{
    [TestFixture]
    public class LogTest
    {
        private SepEventsLogger uut;
        private INewSepEvent testEvent;
        private ITrack T1, T2;

        [SetUp]
        public void SetUp()
        {
            testEvent = Substitute.For<INewSepEvent>();
            T1 = Substitute.For<ITrack>();
            T2 = Substitute.For<ITrack>();
            uut = new SepEventsLogger(testEvent);
        }

        [Test]
        public void WriteFileDoesExist()
        {

            uut.Log("ABC123", "DEF456");

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "SepEventsLog.txt";

            Assert.IsTrue(File.Exists(path));

        }

        [Test]
        public void WriteFileDoesNotExist()
        {

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "SepEventsLog.txt";
            File.Delete(path);
            Assert.IsFalse(File.Exists(path));

            uut.Log("ABC123", "DEF456");

            Assert.IsTrue(File.Exists(path));

        }

        [Test]
        public void LogNewEvent()
        {
            T1.Tag.Returns("T1");
            T2.Tag.Returns("T2");

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "SepEventsLog.txt";
            File.Delete(path);
            Assert.IsFalse(File.Exists(path));

            testEvent.CrashingEvent += Raise.EventWith(new SeperationEventArgs(T1, T2));
            
            Assert.IsTrue(File.Exists(path));

        }
    }
}