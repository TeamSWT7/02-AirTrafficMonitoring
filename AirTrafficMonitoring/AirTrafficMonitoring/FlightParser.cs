using System;
using System.Collections.Generic;
using TransponderReceiver;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class FlightParser : Subject<IFlightParser>, IFlightParser
    {
        private List<Flight> _flights = new List<Flight>();
        public void Update(IFlightTransponderHandler fth)
        {

            string next = fth.GetNext();

            int i = 0;
            int max = 10;

            while (next != null) {
                ParseString(next);

                next = fth.GetNext();

                i++;
                if (i > max) throw new Exception("FlightParser exeeded max iterations in while loop.");
            }

            Notify(this);
        }

        public Flight GetNext()
        {
            if (_flights.Count > 0)
            {
                Flight r = _flights[0];

                _flights.RemoveAt(0);

                Console.WriteLine("GetNext(): " + r);

                return r;
            }

            return null;
        }

        public void ParseString(string s)
        {
            Flight flight = new Flight();

            var strArray = s.Split(';');

            // Validate size of string
            if (strArray.Length != 5)
                return;

            flight.tag = strArray[0];

            // Convert position to integers and save
            flight.position = new Coords(
                int.Parse(strArray[1]),
                int.Parse(strArray[2]),
                int.Parse(strArray[3])
            );

            DateTime? timestamp = ParseDate(strArray[4]);

            // Check if timestamp is valid (not null)
            if (timestamp == null)
                return;
            
            flight.timestamp = (DateTime) timestamp;

            _flights.Add(flight);
        }

        public DateTime? ParseDate(string s)
        {
            if (s.Length != 17) return null;

            int year = int.Parse(s.Substring(0, 4));
            int month = int.Parse(s.Substring(4, 2));
            int day = int.Parse(s.Substring(6, 2));
            int hour = int.Parse(s.Substring(8, 2));
            int min = int.Parse(s.Substring(10, 2));
            int sec = int.Parse(s.Substring(12, 2));
            int ms = int.Parse(s.Substring(14, 3));

            return new DateTime(year, month, day, hour, min, sec, ms);
        }
    }
}