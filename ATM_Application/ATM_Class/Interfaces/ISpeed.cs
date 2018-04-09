using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Interface-klasse til speed, der som et minimum skal bestå af en hastighed og en funktion der kan beregne dette
    public interface ISpeed
    {
        double _speed { get; set; }
        void CalculateSpeed(Position CurrP, Position OldP, Time CurrT, Time OldT);
    }
}
