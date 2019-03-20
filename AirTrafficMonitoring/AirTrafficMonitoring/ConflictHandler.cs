using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring
{
    public class ConflictHandler : IObserver<FlightValidator>
    {
        private List<Conflict> _conflicts = new List<Conflict>();

        public List<Conflict> GetConflicts()
        {
            return _conflicts;
        }

        public void Update(FlightValidator s)
        {
            List<Flight> flightList = new List<Flight>();
            flightList = s.GetList();

            for (int i = 0; i < flightList.Count; i++)
            {
                var flightToCheck = flightList[i];

                for (int j = i+1; j < flightList.Count; j++)
                {
                    CheckForConflicts(flightToCheck, flightList[j]);
                }
            }
        }

        private void CheckForConflicts(Flight flight1, Flight flight2)
        {
            if(CheckHorisontalDistance(flight1, flight2) && CheckVerticalDistance(flight1, flight2))
            {
                Conflict conflict = new Conflict(flight1, flight2);
                _conflicts.Add(conflict);
            }
        }

        private bool CheckHorisontalDistance(Flight flight1, Flight flight2)
        {
            var TempXPow = Math.Pow((flight1.position.x - flight2.position.x),2);
            var TempYPow = Math.Pow((flight1.position.y - flight2.position.y), 2);
            double distance = Math.Sqrt(TempXPow + TempYPow);

            if (distance < 500)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckVerticalDistance(Flight flight1, Flight flight2)
        {
            if ((flight1.altitude - flight2.altitude) < 300)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}