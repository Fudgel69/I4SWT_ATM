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
        private ITransponderReceiver transponderReceiver;


        public AirspaceMonitor(ITransponderReceiver _transponderReceiver)
        {
            transponderReceiver = _transponderReceiver;
            transponderReceiver.TransponderDataReady += transponderReceiver_DataReady;
        }

        public List<Track> Tracks;

        public Track Convert(string trackData)
        {
            
            String[] paths = trackData.Split(';');
            if (int.Parse(paths[1]) >= 10000 && int.Parse(paths[1]) <= 90000 &&
                int.Parse(paths[2]) >= 10000 && int.Parse(paths[2]) <= 90000 &&
                int.Parse(paths[3]) >= 500 && int.Parse(paths[3]) <= 20000)
            {
                int xcord = System.Convert.ToInt32(paths[1]);
                int ycord = System.Convert.ToInt32(paths[2]);
                int altitude = System.Convert.ToInt32(paths[3]);
                string year = paths[4].Substring(0, 4);
                string month = paths[4].Substring(4, 2);
                string day = paths[4].Substring(6, 2);
                string hour = paths[4].Substring(8, 2);
                string minute = paths[4].Substring(10, 2);
                string second = paths[4].Substring(12, 2);
                string milisecond = paths[4].Substring(14, 3);
                DateTime currentTime = new DateTime(System.Convert.ToInt32(year), System.Convert.ToInt32(month), System.Convert.ToInt32(day), System.Convert.ToInt32(hour), System.Convert.ToInt32(minute), System.Convert.ToInt32(second), System.Convert.ToInt32(milisecond));
                return new Track(paths[0], xcord, ycord, altitude, currentTime);
            }
            else
            {
                return;
            }
                
        }

        public void transponderReceiver_DataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var track in e.TransponderData)
            {

                String[] paths = track.Split(';');
                if (int.Parse(paths[1]) >= 10000 && int.Parse(paths[1]) <= 90000 &&
                    int.Parse(paths[2]) >= 10000 && int.Parse(paths[2]) <= 90000 &&
                    int.Parse(paths[3]) >= 500 && int.Parse(paths[3]) <= 20000)
                {
                    int xcord = System.Convert.ToInt32(paths[1]);
                    int ycord = System.Convert.ToInt32(paths[2]);
                    int altitude = System.Convert.ToInt32(paths[3]);
                    string year = paths[4].Substring(0, 4);
                    string month = paths[4].Substring(4, 2);
                    string day = paths[4].Substring(6, 2);
                    string hour = paths[4].Substring(8, 2);
                    string minute = paths[4].Substring(10, 2);
                    string second = paths[4].Substring(12, 2);
                    string milisecond = paths[4].Substring(14, 3);
                    DateTime currentTime = new DateTime(System.Convert.ToInt32(year), System.Convert.ToInt32(month), System.Convert.ToInt32(day), System.Convert.ToInt32(hour), System.Convert.ToInt32(minute), System.Convert.ToInt32(second), System.Convert.ToInt32(milisecond));
                   // return new Track(paths[0], xcord, ycord, altitude, currentTime);

                    if (Tracks.Any(Track => Track.Tag == paths[0])) // checks if track appears multiple times in list
                    {
                        Track AlreadyKnownTrack = (Track)Tracks.First(Track => Track.Tag == paths[0]); // finds existing track with 'Tag'
                        AlreadyKnownTrack.UpdateTrack(paths[0], xcord, ycord, altitude, currentTime); // updated old track instead of making new entry
                        OnTrackEnteredAirspace();
                    }
                    else
                    {
                        Tracks.Add(new Track(paths[0], xcord, ycord, altitude, currentTime);
                    }
                }
                Track ConvertetTrack = Convert(track);
                Console.WriteLine(ConvertetTrack.ToString());
            }
        }
    }
}