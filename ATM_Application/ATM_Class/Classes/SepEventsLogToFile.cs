using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    class SepEventsLogToFile : ILog
    {
        public void Log(string log1, string log2)
        {
            using (StreamWriter sw = File.AppendText(".\\SepEventsLog.txt"))
            {
                sw.WriteLine(log1 + " & " + log2);
            }
        }
    }
}
