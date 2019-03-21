using AirTrafficMonitoring.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    public class AirTrafficMonitor
    {
        private FlightTransponderHandler _flightTransponderHandler;
        private FlightParser _flightParser;
        private FlightHandler _flightHandler;
        private FlightValidator _flightValidator;
        private ConflictHandler _conflictHandler;

        private IPrinter _printer;

        public AirTrafficMonitor(ITransponderReceiver receiver)
        {
            _flightTransponderHandler = new FlightTransponderHandler(receiver);
            _flightParser = new FlightParser();
            _flightHandler = new FlightHandler();
            _flightValidator = new FlightValidator();
            _conflictHandler = new ConflictHandler();

            _flightTransponderHandler.Attach(_flightParser);
            _flightParser.Attach(_flightHandler);
            _flightHandler.Attach(_flightValidator);
            _flightValidator.Attach(_conflictHandler);
        }

        public void SetPrinter(IPrinter printer)
        {
            _conflictHandler.Detach(_printer);

            _printer = printer;

            _conflictHandler.Attach(_printer);
            _printer.SetFlightHandler(_flightHandler);
        }
    }
}
