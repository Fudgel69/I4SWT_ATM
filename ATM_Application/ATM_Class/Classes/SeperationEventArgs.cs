using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class SeperationEventArgs : EventArgs
    {
        public ITrack CrashingTrackOne { get; set; }
        public ITrack CrashingTrackTwo { get; set; }

        public SeperationEventArgs(ITrack tOne, ITrack tTwo)
        {
            CrashingTrackOne = tOne;
            CrashingTrackTwo = tTwo;
        }
    }
}
