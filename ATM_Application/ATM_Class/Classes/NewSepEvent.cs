using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class.Classes
{
    public class NewSepEvent : INotify
    {
        private delegate void SepEventHandler(object sender, SeperationEventArgs e);

        private event SepEventHandler SeperationEvent;

        public void Update(List<Track> f)
        {
            List<Track> _tempList = new List<Track>(f);

            foreach (Track t in f)
            {
                if (CheckAltitude(t, _tempList[0]) && CheckHorizontalSeparation(t, _tempList[0]))
                {
                    var _e = new SeperationEventArgs(t.CurrentTime, t.Tag, _tempList[0].Tag);
                    OnSeparationEvent(_e);
                }

                _tempList.RemoveAt(0);
            }
        }

        private bool CheckAltitude(ITrack track1, ITrack track2)
        {
            return Math.Abs(track1.CurrentPosition.Altitude - track2.CurrentPosition.Altitude) <= 300;
        }

        private void OnSeparationEvent(SeperationEventArgs e)
        {
            var handler = SeperationEvent;
            handler?.Invoke(this, e);
        }

        private bool CheckHorizontalSeparation(ITrack track1, ITrack track2)
        {
            double x = Math.Pow(Math.Abs(track1.CurrentPosition.X - track2.CurrentPosition.X), 2);
            double y = Math.Pow(Math.Abs(track1.CurrentPosition.Y - track2.CurrentPosition.Y), 2);

            return Math.Sqrt(x + y) <= 5000;
        }
    }

}
