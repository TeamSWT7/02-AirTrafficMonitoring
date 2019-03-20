using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public class FlightHandler : Subject<FlightHandler>, IObserver<FlightParser>
    {
        private List<Flight> _flights = new List<Flight>();
        private List<Flight> _newFlights = new List<Flight>();

        public void Update(FlightParser flightParser)
        {
            Flight next = flightParser.GetNext();
            while (next != null)
            {
                _flights.Add(flightParser.GetNext());
            }
            Notify(this);
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}