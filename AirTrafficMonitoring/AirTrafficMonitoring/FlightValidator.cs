using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightValidator : Subject<IFlightHandler>, IFlightValidator
    {
        private List<Flight> _flights;
        private Airspace _airspace;

        public FlightValidator()
        {
            _airspace = new Airspace
            {
                Position = new Coords(0, 0, 500),
                Width = 80000,
                Length = 80000,
                Height = 19500,
            };
        }

        public void Update(IFlightHandler fh)
        {
            _flights = fh.GetFlights();

            for (int i = _flights.Count - 1; i >= 0; i--)
            {
                ValidateFlight(_flights[i]);
            }

            Notify(fh);
        }

        public void ValidateFlight(Flight flight)
        {
            if (!_airspace.InArea(flight))
                _flights.Remove(flight);
        }
    }
}