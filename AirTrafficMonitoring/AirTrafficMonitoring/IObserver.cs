namespace AirTrafficMonitoring
{
    public interface IObserver<T>
    {
        void Update(T s);
    }
}