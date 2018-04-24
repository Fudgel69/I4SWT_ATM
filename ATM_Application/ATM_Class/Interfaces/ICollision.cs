using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public interface ICollision
    {
        DateTime TimeOccurence { get;  }
        List<string> CollisionTag { get; }
        //string ToString();
    }
}
