using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class NewSepEvent : INewSepEvent
    {
        //private readonly ILog _log;
        public event EventHandler<SeperationEventArgs> CrashingEvent;
        public event EventHandler<SeperationEventArgs> NotCrashingEvent;

        public List<Tuple<ITrack, ITrack>> _Crashing = new List<Tuple<ITrack, ITrack>>();

        #region Public Get/Set Metoder
        public List<Tuple<ITrack, ITrack>> Crashing
        {
            get { return _Crashing; }
            set { _Crashing = value; }
        }


        #endregion


        public NewSepEvent()
        {
        }

        //Gennemgår alle fly og ser, om der er nogen der kolliderer
        public void Update(List<ITrack> t)
        {
            for (var i = 0; i < t.Count - 1; ++i)
            {
                for (var x = i + 1; x < t.Count; ++x)
                {
                    if (CheckAltitude(t[x], t[i]) && CheckHorizontalSeparation(t[x], t[i]) && !Crashing.Contains(Tuple.Create(t[i], t[x])))
                    {
                        {
                            //Hvis to fly er ved at kollidere lægges de i Crashing
                            Crashing.Add(Tuple.Create(t[i], t[x]));
                            CrashingEvent?.Invoke(this, new SeperationEventArgs(t[i], t[x]));

                        }
                    }
                }
            }
        }

        //Ser om der er nogle fly der skal fjernes fra Crashing, hvis de ikke kolliderer længere
        public void DoubleCheckCollisions()
        {

            foreach (var CRASH in Crashing.ToArray())
            {
                if ((CheckAltitude(CRASH.Item1, CRASH.Item2) && CheckHorizontalSeparation(CRASH.Item1, CRASH.Item2)) == false)
                {
                    NotCrashingEvent?.Invoke(this, new SeperationEventArgs(CRASH.Item1, CRASH.Item2));
                    Crashing.Remove(CRASH);
                }
            }
        }

        //Returnerer true hvis to fly er indenfor 5000 m horisontalt
        private bool CheckHorizontalSeparation(ITrack track1, ITrack track2)
        {
            double x = Math.Pow(Math.Abs(track1.CurrentPosition.X - track2.CurrentPosition.X), 2);
            double y = Math.Pow(Math.Abs(track1.CurrentPosition.Y - track2.CurrentPosition.Y), 2);

            return Math.Sqrt(x + y) <= 5000;
        }

        //Returnerer true hvis to fly er indenfor 300 m vertikalt
        private bool CheckAltitude(ITrack track1, ITrack track2)
        {
            return Math.Abs(track1.CurrentPosition.Altitude - track2.CurrentPosition.Altitude) <= 300;
        }
    }

}
