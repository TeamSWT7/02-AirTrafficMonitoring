using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class FlightValidatorUnitTest
    {
        private FlightValidator _uut;
        private Flight _flight1;
        private Flight _flight2;
        private Flight _flight3;
        private IFlightHandler _FakeFlightHandler;
        private FlightHandler fh;

        [SetUp]
        public void Setup()
        {
            //ceating fake
            _FakeFlightHandler = Substitute.For<IFlightHandler>();

            _uut = Substitute.For<FlightValidator>();
        }

        [TestCase(16000, 16000, 5000, 1)]   // Inside
        [TestCase(80000, 80000, 20000, 1)]  // On the edge top
        [TestCase(16000, 16000, 25000, 0)]  // Outside
        public void Validate_Flight_inArea_airspace_oneFlight(int x, int y, int z, int result)
        {
            _flight1 = new Flight
            {
                position = new Coords(x, y, z),
            };
          
            _FakeFlightHandler = new FlightHandler();

            // Adding flight to FlightValidator._flights
            _FakeFlightHandler.GetFlights().Add(_flight1);
            _uut.Update(_FakeFlightHandler);
            
            // Removes flight if flight isn't valid
            _uut.ValidateFlight(_flight1);

            // Checks number of flights in FlightValidator._flights
            // Results indicates number of expected flights
            Assert.AreEqual(result, _FakeFlightHandler.GetFlights().Count);

        }

        [TestCase(16000, 16000, 5000, 1)]   // Inside
        [TestCase(80000, 80000, 20000, 1)]  // On the edge top
        [TestCase(16000, 16000, 25000, 0)]  // Outside
        public void Validate_Flight_inArea_airspace_twoFlights(int x, int y, int z, int result)
        {
            _flight2 = new Flight
            {
                position = new Coords(15000, 15000, 9001),
            };
            _flight3 = new Flight
            {
                position = new Coords(x, y, z),
            };

            _FakeFlightHandler = new FlightHandler();

            // Adding flight to FlightValidator._flights
            _FakeFlightHandler.GetFlights().Add(_flight2);
            _FakeFlightHandler.GetFlights().Add(_flight3);
            _uut.Update(_FakeFlightHandler);

            // Removes flight if flight isn't valid
            _uut.ValidateFlight(_flight3);

            // Checks number of flights in FlightValidator._flights
            // Results indicates number of expected flights
            Assert.AreEqual(result+1, _FakeFlightHandler.GetFlights().Count);

        }
    }
}

