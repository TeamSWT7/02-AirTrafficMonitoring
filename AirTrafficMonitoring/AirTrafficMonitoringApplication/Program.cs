using AirTrafficMonitoring;
using System.Threading;
using TransponderReceiver;

namespace AirTrafficMonitoringApplication
{
    class Program
    {
        private static ITransponderReceiver _receiver;

        static void Main(string[] args)
        {
            _receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            Printer printer = new Printer();

            AirTrafficMonitor airTrafficMonitoring = new AirTrafficMonitor(_receiver);

            airTrafficMonitoring.SetPrinter(printer);

            while (true)
                Thread.Sleep(1000);
        }
    }
}
