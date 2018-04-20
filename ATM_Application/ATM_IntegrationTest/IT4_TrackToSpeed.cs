using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using NSubstitute;
using NUnit.Framework;

namespace ATM_IntegrationTest
{
    [TextFixture]
    public class IT4_TrackToSpeed
    {
        private ITrack _track;
        private ITime _time;
        private ISpeed _speed;
        private ICourse _course;
    }
}