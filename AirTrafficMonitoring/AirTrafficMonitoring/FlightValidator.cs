using System.Collections.Generic;


namespace AirTrafficMonitoring
{
    public class FlightValidator : Subject<FlightValidator>, IObserver<FlightHandler>

    {
        private List<Flight> _flights = new List<Flight>();

        public void Update(FlightHandler fh)
        {
            string next = fh.GetFlight();
            while (next != null)
            {
                ParseString(next);

                next = fh.GetFlight();
            }

            Notify(this);
        }

        public Flight GetNext()
        {
            if (_flight.Count > 0)
            {
                string r = _flight[0];

                _flightStrings.RemoveAt(0);

                return r;
            }

            return null;
        }
    }
}