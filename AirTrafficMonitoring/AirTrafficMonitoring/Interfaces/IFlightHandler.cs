using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightHandler
    {
        List<Flight> GetFlights();
    }
}