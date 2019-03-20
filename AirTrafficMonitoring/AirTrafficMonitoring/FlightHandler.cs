using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightHandler : Subject<IFlightHandler>, IFlightHandler
    {
        private List<Flight> _flights = new List<Flight>();
        private List<Flight> _tempFlights = new List<Flight>();

        public void Update(FlightParser flightParser)
        {
            Flight next = flightParser.GetNext();
            while (next != null)
            {
                _tempFlights = _flights;
                UpdateList(next);
                next = flightParser.GetNext();
            }
            Notify(this);
        }

        private void UpdateList(Flight next)
        {
            int f = 0;
            for (int i = 0; i < _flights.Capacity; i++)
            {
                if (next.tag == _tempFlights[i].tag)
                {
                    UpdateFlightInfo(next, _tempFlights[i], _flights[i]);
                    _flights[i] = _tempFlights[i];
                    f = 1;
                }
                else if(i == _flights.Capacity -1 && f != 1)
                {
                    _flights.Add(next);
                }
            }
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}