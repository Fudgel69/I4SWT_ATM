using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class NewSepEvent : INotify
    {

        public event EventHandler<SeperationEventArgs> CrashingEvent;
        public event EventHandler<SeperationEventArgs> NotCrashingEvent;

        public List<Tuple<ITrack, ITrack>> Crashing = new List<Tuple<ITrack, ITrack>>();

        public void Update(List<ITrack> f)
        {
            List<ITrack> _tempList = new List<ITrack>(f);

            foreach (ITrack t in f)
            {
                if (_tempList[0].Tag != t.Tag)
                {
                    if (CheckAltitude(t, _tempList[0]) && CheckHorizontalSeparation(t, _tempList[0]))
                    {
                        Crashing.Add(Tuple.Create(t, _tempList[0]));
                        var _e = new SeperationEventArgs(t, _tempList[0]);
                        CrashingEvent?.Invoke(this, _e);
                    }
                }
                

                _tempList.RemoveAt(0);
            }
        }


        public void DoubleCheckCollisions()
        {
            foreach (var CRASH in Crashing.ToArray()) 
            {
                if (CheckAltitude(CRASH.Item1, CRASH.Item2) && CheckHorizontalSeparation(CRASH.Item1, CRASH.Item2) == false)
                {
                    var _e = new SeperationEventArgs(CRASH.Item1, CRASH.Item2);
                    NotCrashingEvent?.Invoke(this, _e);
                    Crashing.Remove(CRASH);
                    Console.WriteLine($"Flight: {CRASH.Item1.Tag} is on a collisioncourse with Flight: {CRASH.Item2.Tag}");
                }
            }
        }


        private bool CheckHorizontalSeparation(ITrack track1, ITrack track2)
        {
            double x = Math.Pow(Math.Abs(track1.CurrentPosition.X - track2.CurrentPosition.X), 2);
            double y = Math.Pow(Math.Abs(track1.CurrentPosition.Y - track2.CurrentPosition.Y), 2);

            return Math.Sqrt(x + y) <= 5000;
        }

        private bool CheckAltitude(ITrack track1, ITrack track2)
        {
            return Math.Abs(track1.CurrentPosition.Altitude - track2.CurrentPosition.Altitude) <= 300;
        }
    }

}
