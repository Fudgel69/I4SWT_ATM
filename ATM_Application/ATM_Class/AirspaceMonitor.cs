using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM_Class
{
    public class AirspaceMonitor
    {
        ITransponderReceiver _TransponderReceiver;

        public delegate void TrackEnteredAirspaceHandler();
        public delegate void TrackLeftAirspaceHandler();

        public event TrackEnteredAirspaceHandler TrackEnteredAirspace;

        //En liste af trackede fly i vores monitor
        public List<ITrack> Tracks { get; set; }


        //Constructor-klasse for Monitor
        public AirspaceMonitor(ITransponderReceiver transponderReceiver)
        {
            _TransponderReceiver = transponderReceiver;
            Tracks = new List<ITrack>();

            //Subscriber til et event
            transponderReceiver.TransponderDataReady += OnTransponderDataReady; // subscribe to event
        }

        //Et fly er inde i det valgte område
        protected virtual void PlaneEnteredAirspace()
        {
            TrackEnteredAirspace?.Invoke();
            Console.WriteLine("Der kommer FLYYYYY!!");
        }


        private void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // formatting data for use
            foreach (string rawdata in e.TransponderData)
            {
                string[] rawData = rawdata.Split(';');
                List<string> data = rawData.ToList();

                if (int.Parse(data[1]) >= 10000 && int.Parse(data[1]) <= 90000 &&
                    int.Parse(data[2]) >= 10000 && int.Parse(data[2]) <= 90000 &&
                    int.Parse(data[3]) >= 500 && int.Parse(data[3]) <= 20000)
                {
                    if (Tracks.Any(track => track.Tag == data[0])) // checks if track appears multiple times in list
                    {
                        Track AlreadyKnownTrack = (Track)Tracks.First(track => track.Tag == data[0]); // finds existing track with 'Tag'
                        AlreadyKnownTrack.UpdateTrack(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), new Time(data[4])); // updated old track instead of making new entry
                        PlaneEnteredAirspace();
                    }
                    else
                    {
                        Tracks.Add(new Track(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), new Time(data[4])));
                    }

                    foreach (Track t in Tracks)
                    {
                        t.PrintTrack();
                    }
                }

            }
        }
    }
}