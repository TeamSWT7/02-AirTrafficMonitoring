using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public class ConflictHandler : Subject<IConflictHandler>, IConflictHandler
    {
        private readonly List<Conflict> _conflicts = new List<Conflict>();

        private string filename = @"..\Conflicts.txt";

        private int _maxHorizontalDistance;
        private int _maxVerticalDistance;

        private StreamWriter fs;

        public ConflictHandler(int maxHoriDistance = 500, int maxVertDistance = 300)
        {
            _maxHorizontalDistance = maxHoriDistance;
            _maxVerticalDistance = maxVertDistance;

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using (fs = File.CreateText(filename))
            {
                fs.WriteLine("Conflicting flights and time of occurrence");
            }
        }

        public List<Conflict> GetConflicts()
        {
            return _conflicts;
        }

        public void Update(IFlightHandler fh)
        {
            _conflicts.Clear();

            List<Flight> flightList = fh.GetFlights();

            for (int i = 0; i < flightList.Count; i++)
            {
                var flightToCheck = flightList[i];

                for (int j = i + 1; j < flightList.Count; j++)
                {
                    CheckForConflicts(flightToCheck, flightList[j]);
                }
            }

            Notify(this);
        }

        public void CheckForConflicts(Flight flight1, Flight flight2)
        {
            if(CheckHorizontalDistance(flight1, flight2) && CheckVerticalDistance(flight1, flight2))
            {
                Conflict conflict = new Conflict(flight1, flight2);
                _conflicts.Add(conflict);
                WriteToFile(conflict);
            }
        }

        public bool CheckHorizontalDistance(Flight flight1, Flight flight2)
        {
            var TempXPow = Math.Pow((flight1.position.x - flight2.position.x), 2);
            var TempYPow = Math.Pow((flight1.position.y - flight2.position.y), 2);
            double distance = Math.Sqrt(TempXPow + TempYPow);

            return distance <= _maxHorizontalDistance;
        }

        public bool CheckVerticalDistance(Flight flight1, Flight flight2)
        {
            return ((flight1.position.z - flight2.position.z) <= _maxVerticalDistance)
                   && ((flight1.position.z - flight2.position.z) >= -_maxVerticalDistance);
        }

       public void WriteToFile(Conflict conflict)
       {
           using (var fw = new StreamWriter(filename, true))
               {
                   fw.WriteLine(conflict.ToString() + Environment.NewLine);

               }
       }

       public string ReadFromFile()
       {
           string text;
           using (var fw = new StreamReader(filename))
           {
               text = fw.ReadToEnd();
           }
           return text;
       }
    }
}