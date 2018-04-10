using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM_UnitTest
{
    [TestFixture]
    class TrackTest
    {
        private string _tag;
        private string _x;
        private string _y;
        private string _altitude;
        private string _timeStamp;

        private IPosition _position;
        private ITrack _track;

        private List<string> _list;

        [SetUp]
        public void Setup()
        {
            _list = new List<string>{"Simon", "60000", "20000", "5000", "20180409235530065" };
            _tag = _list[0];
            _x = _list[1];
            _y = _list[2];
            _altitude = _list[3];
            _timeStamp = _list[4];
            _position = new Position();
         
            _track = new Track(_tag, int.Parse(_x), int.Parse(_y), int.Parse(_altitude), Time _timeStamp);

        }


    }
}
