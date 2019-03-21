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

        private void UpdateList(Flight next)
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
        private void UpdateFlightInfo(Flight prev, Flight next)
        {
            prev.velocity = CalculateVelocity(prev, next);
            prev.direction = CalculateDirection(prev, next);
            prev.position = next.position;
            prev.timestamp = next.timestamp;
        }
        private double CalculateVelocity(Flight flight1, Flight flight2)
        {
            double distance = Math.Sqrt(Math.Pow((flight2.position.x - flight1.position.x), 2) +
                                        Math.Pow((flight2.position.y - flight1.position.y), 2) +
                                        Math.Pow((flight2.position.z - flight1.position.z), 2));
            TimeSpan timeSpent = flight2.timestamp - flight1.timestamp;

            double velocity = distance / (double) (timeSpent.TotalMilliseconds / 1000.0);

            return velocity;
        }
        private double CalculateDirection(Flight flight1, Flight flight2)
        {
            double direction = Math.Atan2(
                (flight2.position.y - flight1.position.y), 
                (flight2.position.x - flight1.position.x)
                ) * (360 / Math.PI * 2);
            return direction;
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}