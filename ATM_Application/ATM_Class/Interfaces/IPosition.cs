using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Interface-klasse til position, der som minimum skal bestå af X- og Y-koordinater
    public interface IPosition
    {
        int X { get; set; }
        int Y { get; set; }
    }
}
