using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM_UnitTest
{
    [TestFixture]
    class CollisionTest
    {

        private NewSepEvent SepEvent;
        private ITrack T1, T2;
        private Position pos;

        private int _x = 20000;
        private int _y = 20000;
        private int _altitude = 10000;

        [SetUp]
        public void SetUp()
        {
            pos = new Position();
            pos.SetPosition(_x, _y, _altitude);
            SepEvent = new NewSepEvent();
            T1 = new Track("T1", pos, new Time());
            T2 = new Track("T2", pos, new Time());
        }

        //Fly der ikke længere er i kollisionsfare bliver fjernet fra listen
        [Test]
        public void UpdateRemovesPlanesOutOfDanger()
        {
            
            T1.CurrentPosition.SetPosition(10000, 10000, 100000);
            SepEvent.Crashing.Add(Tuple.Create(T1, T2));
            
            List<Tuple<ITrack, ITrack>> Crashing = new List<Tuple<ITrack, ITrack>>(SepEvent.Crashing);
            
            SepEvent.DoubleCheckCollisions();

            List<Tuple<ITrack, ITrack>> NotCrashing = SepEvent.Crashing;

            Assert.That(Crashing.Count > NotCrashing.Count, Is.EqualTo(true));
            


        }
    }
}
