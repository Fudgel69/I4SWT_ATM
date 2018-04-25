using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Class
{
    public interface ILog
    {
        void Log(string log1, string log2);
        void CrashingSepHandler(object sender, SeperationEventArgs args);
    }
}
