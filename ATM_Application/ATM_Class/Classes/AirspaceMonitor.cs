using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using TransponderReceiver;

namespace ATM_Class
{
    public class AirspaceMonitor
    {
        public ITransponderReceiver _TransponderReceiver;

        public delegate void TrackEnteredAirspaceHandler();
        public delegate void TrackLeftAirspaceHandler();

        public event TrackEnteredAirspaceHandler TrackEnteredAirspace;

        private INewSepEvent _CrashTester { get; set; }

        //En liste af trackede fly i vores monitor
        private List<ITrack> _Tracks { get; set; }


        public INewSepEvent CrashTester
        {
            get { return _CrashTester;}
            set { _CrashTester = value; }
        }
        public List<ITrack> Tracks
        {
            get { return _Tracks; }
        }


        //Constructor-klasse for Monitor
        public AirspaceMonitor(ITransponderReceiver transponderReceiver)
        {
            CrashTester = new NewSepEvent();

            CrashTester.CrashingEvent += DetectSeperation;
            CrashTester.NotCrashingEvent += DetectNoSeperation;
            _TransponderReceiver = transponderReceiver;
            _Tracks = new List<ITrack>();

            //Subscriber til et event
            transponderReceiver.TransponderDataReady += TransponderDataEvent; 
        }


        private void DetectNoSeperation(object sender, SeperationEventArgs e)
        {
            e.CrashingTrackOne.Crashing = false;
            e.CrashingTrackTwo.Crashing = false;

        }

        private void DetectSeperation(object sender, SeperationEventArgs e)
        {
            e.CrashingTrackOne.Crashing = true;
            e.CrashingTrackTwo.Crashing = true;
        }


        public void CreateOrUpdate(List<String> data)
        {
            Position _pos = new Position();
            _pos.X = int.Parse(data[1]);
            _pos.Y = int.Parse(data[2]);
            _pos.Altitude = int.Parse(data[3]);
            if (_Tracks.Any(track => track.Tag == data[0]))
            {
                //Hvis flyet allerede er tracket, vil dennes data blive opdateret
                Track TrackToUpdate = (Track)_Tracks.First(track => track.Tag == data[0]);
                TrackToUpdate.UpdateTrack(data[0],_pos , new Time(data[4]));
            }
            else
            {
                //Hvis ikke flyet eksisterer vil der blive skabt et nyt track af dette
                _Tracks.Add(new Track(data[0], _pos, new Time(data[4])));
            }
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
                    CreateOrUpdate(data);

                    //Printer alle fly i Monitor-zonen når et fly bliver opdateret
                    foreach (Track t in _Tracks)
                    {
                        t.PrintTrack();
                    }
                }
            }
            CrashTester.DoubleCheckCollisions();
            CrashTester.Update(_Tracks);
        }
    }
}