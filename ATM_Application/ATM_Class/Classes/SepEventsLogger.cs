using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public class SepEventsLogger : ILog
    {
        private string _filePath = string.Empty;
        private string _fileName = string.Empty;

        public SepEventsLogger(INewSepEvent newSepEvent)
        {
            newSepEvent.CrashingEvent += CrashingSepHandler;
        }

        public SepEventsLogger()
        {
        }

        public void Log(string log1, string log2)
        {
            _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _fileName = Path.Combine(_filePath, "SepEventsLog.txt");

            if (!File.Exists(_fileName))
            {
                using (FileStream fs = File.Create(_fileName))
                {
                    Byte[] info =
                        new UTF8Encoding(true).GetBytes("COLLISION COURSE WARNINGS:");
                    fs.Write(info, 0, info.Length);
                }
            }

            else
            {
                using (StreamWriter sw = File.AppendText(_filePath + "\\" + "SepEventsLog.txt"))
                {
                    sw.WriteLine($"Flight: {0} is on a collisioncourse with Flight: {1}", log1, log2);
                    sw.WriteLine($"Time of collision course occurence {0}", DateTime.Now);
                }
            }

            
        }

        public void CrashingSepHandler(object sender, SeperationEventArgs args)
        {
            var sep1 = args.CrashingTrackOne.Tag;
            var sep2 = args.CrashingTrackTwo.Tag;
            Log(sep1, sep2);
        }

    }
}
