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
    class CourseTest
    {
        Course CurCourse = new Course();
        Position CurPos = new Position();
        Position OldPos = new Position();

        [SetUp]
        public void Setup()
        {
            
        }


        [Test]
        public void TestCalculateCourseX()
        {
            CurPos.SetPosition(20000, 20000, 10000);
            OldPos.SetPosition(10000, 20000, 10000);

            double expectedCourse = 90;
            CurCourse.CalculateCourse(CurPos, OldPos);
            Assert.That(CurCourse._course, Is.EqualTo(expectedCourse));

        }

        [Test]
        public void TestCalculateCourseY()
        {
            CurPos.SetPosition(20000, 20000, 10000);
            OldPos.SetPosition(20000, 10000, 10000);

            double expectedCourse = 0;
            CurCourse.CalculateCourse(CurPos, OldPos);
            Assert.That(CurCourse._course, Is.EqualTo(expectedCourse));

        }

        [Test]
        public void TestCalculateCourseXY()
        {
            CurPos.SetPosition(20000, 20000, 10000);
            OldPos.SetPosition(10000, 10000, 10000);

            double expectedCourse = 45;
            CurCourse.CalculateCourse(CurPos, OldPos);
            Assert.That(CurCourse._course, Is.EqualTo(expectedCourse));

        }


    }
}
