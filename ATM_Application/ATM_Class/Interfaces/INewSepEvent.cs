using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public interface INewSepEvent
    {

        event EventHandler<SeperationEventArgs> CrashingEvent;
        event EventHandler<SeperationEventArgs> NotCrashingEvent;

        void Update(List<ITrack> f);
        void DoubleCheckCollisions();
    }
}