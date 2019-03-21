using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightHandler : ISubject<IFlightHandler>, IObserver<IFlightParser>
    {
        List<Flight> GetFlights();
        void UpdateList(Flight next);
        void UpdateFlightInfo(Flight prev, Flight next);
        double CalculateVelocity(Flight flight1, Flight flight2);

    }
}