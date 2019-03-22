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
                    f = 1;
                }
                else if(i == _flights.Capacity -1 && f != 1)
                {
                    _flights.Add(next);
                }
            }
        }
        private void UpdateFlightInfo(Flight next, Flight temp, Flight flight)
        {
            flight.position = next.position;
            flight.timestamp = next.timestamp;
            flight.velocity = CalculateVelocity(next, temp);
            flight.direction = CalculateDirection(next, temp);
        }
        private double CalculateVelocity(Flight next, Flight temp)
        {
            double distance = Math.Sqrt((temp.position.x - next.position.x) ^ 2 + (temp.position.y - next.position.y) ^
                                        2 + (temp.position.z - next.position.z) ^ 2);
            TimeSpan timeSpent = next.timestamp - temp.timestamp;
            double velocity = distance / timeSpent.Seconds;
            return velocity;
        }
        private double CalculateDirection(Flight next, Flight temp)
        {
            double direction = Math.Atan2((next.position.y - temp.position.y), (next.position.x - temp.position.x));
            return direction;
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}