using TransponderReceiver;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IFlightTransponderHandler : ISubject<IFlightTransponderHandler>
    {
        string GetNext();

        void OnReceivedData(object sender, RawTransponderDataEventArgs e);
    }
}