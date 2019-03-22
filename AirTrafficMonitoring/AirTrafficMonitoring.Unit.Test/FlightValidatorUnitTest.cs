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
        private IFlightHandler _fakeFlightHandler;
        private List<Flight> _validFlights;
        private List<Flight> _oneInvalidFlights;
        private List<Flight> _twoInvalidFlights;

        [SetUp]
        public void Setup()
        {
            //Ceating fake
            _fakeFlightHandler = Substitute.For<IFlightHandler>();

            _uut = Substitute.For<FlightValidator>();

            _validFlights = new List<Flight>()
            {
                new Flight()
                {
                    position = new Coords(16000, 16000, 5000)
                },
                new Flight()
                {
                    position = new Coords(8000, 8000, 20000)
                },
                new Flight()
                {
                    position = new Coords(8000, 8000,500)
                }             
            };

            _oneInvalidFlights = new List<Flight>()
            {
                new Flight()
                {
                    position = new Coords(16000, 16000, 5000)
                },
                new Flight()
                {
                    position = new Coords(8000, 8000, 25000) // Invalid, z too high
                },
                new Flight()
                {
                    position = new Coords(8000, 8000,500)
                }
            };

            _twoInvalidFlights = new List<Flight>()
            {
                new Flight()
                {
                    position = new Coords(16000, 16000, 5000)
                },
                new Flight()
                {
                    position = new Coords(8000, 8000, 25000)
                },
                new Flight()
                {
                    position = new Coords(8000, 8000,400) // Invalid, z too low
                }
            };
        }

        [Test]
        public void Update_ValidFlights_NoneIsRemoved()
        {

            _fakeFlightHandler.GetFlights().Returns(_validFlights);
           
            _uut.Update(_fakeFlightHandler);
            
            // Checks number of flights in FlightValidator._flights
            // Results indicates number of expected flights
            Assert.AreEqual(3, _fakeFlightHandler.GetFlights().Count);
        }

        [Test]
        public void Update_InvalidFlights_OneIsRemoved()
        {

            _fakeFlightHandler.GetFlights().Returns(_oneInvalidFlights);

            _uut.Update(_fakeFlightHandler);

            // Checks number of flights in FlightValidator._flights
            // Results indicates number of expected flights
            Assert.AreEqual(2, _fakeFlightHandler.GetFlights().Count);
        }

        [Test]
        public void Update_InvalidFlights_TwoeIsRemoved()
        {

            _fakeFlightHandler.GetFlights().Returns(_twoInvalidFlights);

            _uut.Update(_fakeFlightHandler);

            // Checks number of flights in FlightValidator._flights
            // Results indicates number of expected flights
            Assert.AreEqual(1, _fakeFlightHandler.GetFlights().Count);
        }

        [Test]
        public void Update_ValidFlights_EmptyList()
        {
            // Gets empty list and returns that
            _fakeFlightHandler.GetFlights().Returns(new List<Flight>());

            _uut.Update(_fakeFlightHandler);

            // Checks number of flights in FlightValidator._flights
            // Results indicates number of expected flights
            Assert.AreEqual(0, _fakeFlightHandler.GetFlights().Count);
        }

    }
}

