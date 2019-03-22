using System.Collections.Generic;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IConflictHandler : Interfaces.IObserver<FlightValidator>
    {
        List<Conflict> GetConflicts();
        void CheckForConflicts(Flight flight1, Flight flight2);
        bool CheckHorizontalDistance(Flight flight1, Flight flight2);
        bool CheckVerticalDistance(Flight flight1, Flight flight2);
    }
}