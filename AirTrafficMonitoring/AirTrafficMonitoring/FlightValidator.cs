using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightValidator : Subject<IFlightHandler>, IFlightValidator
    {
        private List<Flight> _flights;

        public void Update(IFlightHandler fh)
        {
            //gå igennem liste og tjek om de er indenfor x/y/z

            _flights = fh.GetFlights();

            /*
            foreach (var flight in _flights)
            {
                if (flight.position.x > airspace.x + airspace.width || flight.position.x < airspace.x)
                {
                    _flights.Remove(flight);
                }

                if (flight.position.y > airspace.y + airspace.length || flight.position.y < airspace.y)
                {
                    _flights.Remove(flight);
                }

                if (flight.position.z > airspace.z + airspace.height || flight.position.z < airspace.z)
                {
                    _flights.Remove(flight);
                }
            }
            */

            /*
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
            */

            for (int i = _flight.Count - 1; i >= 0; i--)
            {
                if ( airspace.inArea = 80000)
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


            Notify(fh);
        }
    }
}