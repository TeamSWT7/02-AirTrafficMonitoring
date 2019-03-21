using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoringApplication
{
    public class Printer : IPrinter
    {
        private IFlightHandler _flightHandler;

        public void Update(IConflictHandler ch)
        {
            if (_flightHandler == null)
                throw new Exception("FlightHandler hasn't been attached. Use SetFlightHandler() to attach it.");
            
            List<Flight> flights = _flightHandler.GetFlights();
            List<Conflict> conflicts = ch.GetConflicts();

            // Console.Clear();

            Console.WriteLine("Flights:");

            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
            
            Console.WriteLine();
            Console.WriteLine("Conflicts:");

            foreach (var conflict in conflicts)
            {
                Console.WriteLine(conflict);
            }
        }

        public void SetFlightHandler(IFlightHandler handler)
        {
            _flightHandler = handler;
        }
    }
}
