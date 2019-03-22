using System.Collections.Generic;
using TransponderReceiver;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightTransponderHandler : Subject<IFlightTransponderHandler>, IFlightTransponderHandler
    {
        private ITransponderReceiver _receiver;
        private List<string> _flightStrings;

        public FlightTransponderHandler(ITransponderReceiver receiver)
        {
            _receiver = receiver;
            _receiver.TransponderDataReady += OnReceivedData;

            _flightStrings = new List<string>();
        }

        public string GetNext()
        {
            if (_flightStrings.Count > 0) {
                string r = _flightStrings[0];

                _flightStrings.RemoveAt(0);

                return r;
            }

            return null;
        }

        public void OnReceivedData(object sender, RawTransponderDataEventArgs e)
        {
            _flightStrings.AddRange(e.TransponderData);

            Notify(this);
        }
    }
}