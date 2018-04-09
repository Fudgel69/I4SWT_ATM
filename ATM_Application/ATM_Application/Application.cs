using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Class;
using TransponderReceiver;

namespace ATM_Application
{
    class Application
    {
        //Mainprogrammet opretter et nyt luftrum som kan blive overvåget
        static void Main(string[] args)
        {
            AirspaceMonitor flightConverter = new AirspaceMonitor(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Console.ReadLine();
        }
    }
}
