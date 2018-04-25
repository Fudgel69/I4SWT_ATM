using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Interface til Track-klasse der som et minimum skal bestå af en position, et tag, en kurs og en hastighed
    public interface ITrack
    {
        string Tag { get; set; }
        Course CurrentCourse { get; set; }
        Position CurrentPosition { get; set; }
        Speed CurrentSpeed { get; set; }
        Time CurrentTime { get; set; }
        bool Crashing { get; set; }

        void UpdateTrack(string tag, Position _pos, Time _time);
    }
}
