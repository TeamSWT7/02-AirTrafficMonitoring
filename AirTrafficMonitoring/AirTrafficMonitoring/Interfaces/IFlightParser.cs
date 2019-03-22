using System;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightParser : ISubject<IFlightParser>, Interfaces.IObserver<IFlightTransponderHandler>
    {
        Flight GetNext();
        void ParseString(string s);
        DateTime? ParseDate(string s);
    }
}