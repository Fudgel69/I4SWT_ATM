using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    //Interface-klasse til time der som et minimunm skal bestå af timer og minutter
    public interface ITime
    {
        int Hour { get; set; }
        int Minute { get; set; }

    }
}
