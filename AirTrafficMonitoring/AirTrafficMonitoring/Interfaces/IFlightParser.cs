namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightParser : ISubject<IFlightParser>, IObserver<IFlightTransponderHandler>
    {
         
    }
}