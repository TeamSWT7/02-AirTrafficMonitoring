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
                _newFlights.Add(flightParser.GetNext());
                UpdateList(_newFlights, _flights);
                next = flightParser.GetNext();
            }
            Notify(this);
        }

        private void UpdateList(List<Flight> newList, List<Flight> list)
        {
            if (list.Capacity == 0)
            {
                list = newList;
            }
            else
            {
                foreach (var fligth in newList)
                {
                    
                }
            }
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}