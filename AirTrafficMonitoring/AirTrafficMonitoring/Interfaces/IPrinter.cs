using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Interfaces
{
    public interface IPrinter : IObserver<IConflictHandler>
    {
        void SetFlightHandler(IFlightHandler handler);
    }
}
