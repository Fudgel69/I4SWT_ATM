using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public interface ITrack
    {
        string Tag { get; set; }
        Course CurrentCourse { get; set; }
        Position CurrentPosition { get; set; }
        Speed CurrentSpeed { get; set; }
    }
}
