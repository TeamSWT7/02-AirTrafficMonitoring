using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightValidator : ISubject<IFlightHandler>, IObserver<IFlightHandler>
    {
        
    }
}