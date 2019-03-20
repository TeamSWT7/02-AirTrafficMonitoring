using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightHandler : ISubject<IFlightHandler>, Interfaces.IObserver<FlightParser>
    {
        List<Flight> GetFlights();
    }
}