using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AirTrafficMonitoring.Interfaces;


namespace AirTrafficMonitoring
{
    public class FlightValidator : Subject<IFlightValidator>, IFlightValidator
    {
        private List<Flight> _flights;

        public void Update(FlightHandler fh)
        {
            //gå igennem liste og tjek om de er indenfor x/y/z

            _flights = fh.GetFlights();

            foreach (var flight in _flights)
            {
                if (flight.position.x > 80000)
                {
                    _flights.Remove(flight);
                }

                if (flight.position.y > 80000)
                {
                    _flights.Remove(flight);
                }

                if (flight.position.z <= 500 || flight.position.z >= 20000)
                {
                    _flights.Remove(flight);
                }
            }

            Notify(this);
        }
    }
}