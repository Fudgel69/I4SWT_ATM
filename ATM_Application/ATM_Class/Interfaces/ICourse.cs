using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Interface-klasse til couse der mindst skal bestå af en kurs, og en funktion der kan beregne denne
    public interface ICourse
    {
        double _course { get; set; }

        void CalculateCourse(Position CurrentPosition, Position OldPosition);
    }
}
