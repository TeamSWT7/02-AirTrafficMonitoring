using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightHandler : ISubject<IFlightHandler>, IObserver<IFlightParser>
    {
        List<Flight> GetFlights();
        void HandleFlight(Flight next);
        void UpdateFlightInfo(Flight prev, Flight next);
        double CalculateVelocity(Flight prev, Flight next);
        double CalculateDirection(Flight prev, Flight next);
    }
}