﻿using System;
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

        private INotify _CrashTester { get; set; }

        //En liste af trackede fly i vores monitor
        private List<ITrack> _Tracks { get; set; }


        public INotify CrashTester
        {
            get { return _CrashTester;}
            set { _CrashTester = value; }
        }
        public List<ITrack> Tracks
        {
            get { return _Tracks;}
            set { _Tracks = value; }
        }


        //Constructor-klasse for Monitor
        public AirspaceMonitor(ITransponderReceiver transponderReceiver)
        {
            _TransponderReceiver = transponderReceiver;
            Tracks = new List<ITrack>();
            CrashTester = new NewSepEvent();

            CrashTester.CrashingEvent += DetectorOnSeparationEvent;
            CrashTester.NotCrashingEvent += DetectorOnNoSeperationEvent;
            //Subscriber til et event
            transponderReceiver.TransponderDataReady += TransponderDataEvent; 
        }


        private void DetectorOnNoSeperationEvent(object sender, SeperationEventArgs e)
        {
            e.CrashingTrackOne.Crashing = false;
            e.CrashingTrackTwo.Crashing = false;

        }

        private void DetectorOnSeparationEvent(object sender, SeperationEventArgs e)
        {
            e.CrashingTrackOne.Crashing = true;
            e.CrashingTrackOne.OldCrashTime = e.CrashingTrackOne.CrashTime;
            e.CrashingTrackTwo.Crashing = true;
            e.CrashingTrackTwo.OldCrashTime = e.CrashingTrackOne.CrashTime;
        }


        //Et fly er inde i det valgte område
        protected virtual void PlaneEnteredAirspace()
        {
            TrackEnteredAirspace?.Invoke();
            Console.WriteLine("Der kommer FLYYYYY!!");
        }


        public void CreateOrUpdate(List<String> data)
        {
            Position _pos = new Position();
            _pos.X = int.Parse(data[1]);
            _pos.Y = int.Parse(data[2]);
            _pos.Altitude = int.Parse(data[3]);
            if (Tracks.Any(track => track.Tag == data[0]))
            {
                //Hvis flyet allerede er tracket, vil dennes data blive opdateret
                Track TrackToUpdate = (Track)Tracks.First(track => track.Tag == data[0]);
                TrackToUpdate.UpdateTrack(data[0],_pos , new Time(data[4]));
                PlaneEnteredAirspace();
            }
            else
            {
                //Hvis ikke flyet eksisterer vil der blive skabt et nyt track af dette
                Tracks.Add(new Track(data[0], _pos, new Time(data[4])));
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
                    foreach (Track t in Tracks)
                    {
                        t.PrintTrack();
                    }
                }
                CrashTester.DoubleCheckCollisions();
                CrashTester.Update(Tracks);
            }
        }
    }
}