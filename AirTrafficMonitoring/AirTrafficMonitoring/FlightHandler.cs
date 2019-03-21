using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightHandler : Subject<IFlightHandler>, IFlightHandler
    {
        private List<Flight> _flights = new List<Flight>();

        public void Update(IFlightParser flightParser)
        {
            Flight next = flightParser.GetNext();
            while (next != null)
            {
                UpdateList(next);
                next = flightParser.GetNext();
            }
            Notify(this);
        }

        public void UpdateList(Flight next)
        {
            foreach(var flight in _flights)
            {
                if (flight.tag == next.tag)
                {
                    UpdateFlightInfo(flight, next);
                    return;
                }
            }
            _flights.Add(next);
        }
        public void UpdateFlightInfo(Flight prev, Flight next)
        {
            prev.velocity = CalculateVelocity(prev, next);
            prev.direction = CalculateDirection(prev, next);
            prev.position = next.position;
            prev.timestamp = next.timestamp;
        }
        public double CalculateVelocity(Flight prev, Flight next)
        {
            double distance = Math.Sqrt(Math.Pow((next.position.x - prev.position.x), 2) +
                                        Math.Pow((next.position.y - prev.position.y), 2) +
                                        Math.Pow((next.position.z - prev.position.z), 2));
            TimeSpan timeSpent = next.timestamp - prev.timestamp;

            double velocity = distance / (double) (timeSpent.TotalMilliseconds / 1000.0);

            return velocity;
        }
        public double CalculateDirection(Flight prev, Flight next)
        {
            double direction = Math.Atan2(
                (next.position.y - prev.position.y), 
                (next.position.x - prev.position.x)
                ) * 180 / Math.PI;
            return direction = (direction + 360) % 360;
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}