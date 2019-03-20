using System.Collections.Generic;
using AirTrafficMonitoring.Interfaces;

namespace AirTrafficMonitoring
{
    public abstract class Subject<T> : ISubject<T>
    {
        public List<IObserver<T>> _observers;

        public Subject() {
            _observers = new List<IObserver<T>> ();
        }

        public void Attach(IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(T s)
        {
            foreach (var observer in _observers)
            {
                observer.Update(s);
            }
        }
    }
}