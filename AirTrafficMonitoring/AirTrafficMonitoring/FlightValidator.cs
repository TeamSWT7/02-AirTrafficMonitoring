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
            if (_flights.Count > 0)
            {
                Flight r = _flights[0];

                _flights.RemoveAt(0);

                return r;
            }

            return null;
        }
    }
}