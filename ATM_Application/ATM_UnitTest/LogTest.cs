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

namespace ATM_UnitTest
{
    [TestFixture]
    public class LogTest
    {

        [SetUp]
        public void SetUp()
        {
           
        }

        [Test]
        public void WriteFileDoesExist()
        {
            var uut = new SepEventsLogger();
            uut.Log("ABC123", "DEF456");

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "SepEventsLog.txt";

            Assert.IsTrue(File.Exists(path));

        }

        [Test]
        public void WriteFileDoesNotExist()
        {
            var uut = new SepEventsLogger();
            uut.Log("ABC123", "DEF456");
            
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "NotSepEventsLog.txt";

            Assert.IsFalse(File.Exists(path));

        }
    }
}