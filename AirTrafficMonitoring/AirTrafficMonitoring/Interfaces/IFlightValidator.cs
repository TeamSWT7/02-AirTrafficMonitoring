using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightValidator : ISubject<IFlightValidator>, Interfaces.IObserver<FlightHandler>
    {
        
    }
}