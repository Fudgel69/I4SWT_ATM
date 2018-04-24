using ATM_Class;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM_IntegrationTest
{
    [TestFixture]
    public class IT2_TrackToCourse
    {
        private Position _position;
        private Time _time;
        private ITrack _track;
        private ICourse _course;

        [SetUp]
        public void SetUp()
        {
            _position = new Position();
            _position.X = 20000;
            _position.Y = 20000;
            _position.Altitude = 5000;
            _course = new Course();
            _time = Substitute.For<Time>();
            _track = new Track("Flight", _position, _time );
        }

        //Tester at hvis positionen bliver lavet om, vil der også blive lavet en kurs
        [Test]
        public void TestCourseCorrect()
        {
            _position.X = 10000;
            _position.Y = 10000;
            _position.Altitude = 5000;
            _track.UpdateTrack("Flight", _position, _time);
            Assert.That(_track.CurrentCourse._course, Is.EqualTo(225));
        }
    }
}