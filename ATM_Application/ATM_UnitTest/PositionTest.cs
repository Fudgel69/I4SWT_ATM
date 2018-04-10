using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NUnit.Framework;
using NSubstitute;


namespace ATM_UnitTest
{
    [TestFixture]
    public class PositionTest
    {
        private IPosition _position;

        [SetUp]
        public void Setup()
        {
            _position = new Position();
        }

        //Test Set X
        [Test]
        public void Position_SetXCoordinate_ReturnXCoordinate()
        {
            int x = 40000;
            _position.SetPosition(x, 10000, 5000);
            Assert.AreEqual(x,_position.X);

        }

        //Test Set y
        [Test]
        public void Position_SetYCoordinate_ReturnYCoordinate()
        {
            int y = 10000;
            _position.SetPosition(4000, y, 5000);
            Assert.AreEqual(y, _position.Y);
        }

        //Test Set Altitude
        [Test]
        public void Position_SetAltCoordinate_ReturnAltCoordinate()
        {
            int alt = 5000;
            _position.SetPosition(4000, 10000, alt);
            Assert.AreEqual(alt, _position.Altitude);
        }




    }
}
