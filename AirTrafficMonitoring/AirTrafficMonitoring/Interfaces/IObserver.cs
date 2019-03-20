namespace AirTrafficMonitoring.Interfaces
{
    public interface IObserver<T>
    {
        void Update(T s);
    }
}