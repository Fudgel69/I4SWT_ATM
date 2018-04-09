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
        static void Main(string[] args)
        {
            //ITransponderReceiver navn = TransponderReceiverFactory.CreateTransponderDataReceiver();

            //navn.TransponderDataReady += navn_DataReady;
            //Console.ReadKey();
            AirspaceMonitor flightConverter = new AirspaceMonitor(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Console.ReadLine();

        }

        static void navn_DataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var VARIABLE in e.TransponderData)
            {
                Console.WriteLine(VARIABLE);
            }
        }
    }
}
