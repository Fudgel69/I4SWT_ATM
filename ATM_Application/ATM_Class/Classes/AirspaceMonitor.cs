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
            transponderReceiver.TransponderDataReady += TransponderDataEvent; 
        }

        //Et fly er inde i det valgte område
        protected virtual void PlaneEnteredAirspace()
        {
            TrackEnteredAirspace?.Invoke();
            Console.WriteLine("Der kommer FLYYYYY!!");
        }


        private void TransponderDataEvent(object sender, RawTransponderDataEventArgs e)
        {
            foreach (String planeData in e.TransponderData)
            {
                //Formaterer dataen ved at splitte strengen hvor der bliver fundet et ';'
                List<String>data = planeData.Split(';').ToList();

                //Sikrer sig at flyet er i Monitor-zonen
                if (int.Parse(data[1]) >= 10000 && int.Parse(data[1]) <= 90000 && int.Parse(data[2]) >= 10000 && int.Parse(data[2]) <= 90000 && int.Parse(data[3]) >= 500 && int.Parse(data[3]) <= 20000)
                {
                    //Finder ud af om flyet allerede er tracket
                    if (Tracks.Any(track => track.Tag == data[0]))
                    {
                        //Hvis flyet allerede er tracket, vil dennes data blive opdateret
                        Track TrackToUpdate = (Track)Tracks.First(track => track.Tag == data[0]); 
                        TrackToUpdate.UpdateTrack(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), new Time(data[4])); 
                        PlaneEnteredAirspace();
                    }
                    else
                    {
                        //Hvis ikke flyet eksisterer vil der blive skabt et nyt track af dette
                        Tracks.Add(new Track(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), new Time(data[4])));
                    }

                    //Printer alle fly i Monitor-zonen når et fly bliver opdateret
                    foreach (Track t in Tracks)
                    {
                        t.PrintTrack();
                    }
                }

            }
        }
    }
}