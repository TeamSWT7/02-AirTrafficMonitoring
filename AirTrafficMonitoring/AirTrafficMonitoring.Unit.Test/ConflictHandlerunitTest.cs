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
    public class ConflictHandlerUnitTest
    {
        #region Setup
        private ConflictHandler _uut;
        private IFlightValidator _FakeFlightValidator;
        private IFlightHandler _FakeFlightHandler;
        private Flight _flight1;
        private Flight _flight2;
        private List<Flight> testFlightsWithConflicts;
        private List<Flight> testFlightsNoConflicts;
        private List<Flight> EmptyListFlights;

        [SetUp]
        public void Setup()
        {
            // Create fakes
            _FakeFlightHandler = Substitute.For<IFlightHandler>();
            _FakeFlightValidator = Substitute.For<IFlightValidator>();

            _uut = Substitute.For<ConflictHandler>(300, 500);

            EmptyListFlights = new List<Flight>();

            testFlightsWithConflicts = new List<Flight>()
            {
                new Flight()
                {
                    tag = "ATR423",
                    position = new Coords(10005, 8000, 14000),
                },
                new Flight()
                {
                    tag = "DTY420",
                    position = new Coords(14005, 15000, 15500),
                },
                new Flight()
                {
                    tag = "PRY696",
                    position = new Coords(10105, 7900, 13600),
                },
            };

            testFlightsNoConflicts = new List<Flight>()
            {
                new Flight()
                {
                    tag = "ATR423",
                    position = new Coords(75005, 15600, 14056),
                },
                new Flight()
                {
                    tag = "DTY420",
                    position = new Coords(50605, 20654, 18860),
                },
                new Flight()
                {
                    tag = "PRY696",
                    position = new Coords(11005, 7900, 13600),
                },
            };

        }

        #endregion

        #region VerticalDistanceTests

        [Test]
        public void CheckVerticalDistance_FlightInRange_ResultIsTrue()
        {
            _flight1 = new Flight
            {
                position = new Coords(15500, 15500, 16000),
            };
            _flight2 = new Flight
            {
                position = new Coords(15600, 15450, 15900),
            };

            Assert.IsTrue(_uut.CheckVerticalDistance(_flight1, _flight2));
        }

        [Test]
        public void CheckVerticalDistance_FlightNotInRange_ResultIsFalse()
        {
            _flight1 = new Flight
            {
                position = new Coords(15500, 15500, 19000),
            };
            _flight2 = new Flight
            {
                position = new Coords(15600, 15450, 11000),
            };

            Assert.IsFalse(_uut.CheckVerticalDistance(_flight1, _flight2));
        }

        #endregion

        #region HorizontalDistanceTests
        
        [Test]
        public void CheckHorizontalDistance_FlightInRange_ResultIsTrue()
        {
            _flight1 = new Flight
            {
                position = new Coords(15500, 15500, 16000),
            };
            _flight2 = new Flight
            {
                position = new Coords(15600, 15450, 15900),
            };

            Assert.IsTrue(_uut.CheckHorizontalDistance(_flight1, _flight2));
        }

        [Test]
        public void CheckHorizontalDistance_FlightNotInRange_ResultIsFalse()
        {
            _flight1 = new Flight
            {
                position = new Coords(87000, 15500, 16000),
            };
            _flight2 = new Flight
            {
                position = new Coords(15600, 15450, 15900),
            };

            Assert.IsFalse(_uut.CheckHorizontalDistance(_flight1, _flight2));
        }

        #endregion

        #region UpdateTests

        [Test]
        public void Update_EmptyList_ListIsEmpty()
        {
            _FakeFlightHandler.GetFlights().Returns(EmptyListFlights);

            _uut.Update(_FakeFlightHandler);

            Assert.AreEqual(_uut.GetConflicts(), EmptyListFlights);
        }
        
        [Test]
        public void Update_ListWithNoConflicts_ConflictListIsEmpty()
        {
            _FakeFlightHandler.GetFlights().Returns(testFlightsNoConflicts);

            _uut.Update(_FakeFlightHandler);

            Assert.IsEmpty(_uut.GetConflicts());
        }

        [Test]
        public void Update_ListWithConflicts_ConflictListIsNotEmpty()
        {
            _FakeFlightHandler.GetFlights().Returns(testFlightsWithConflicts);

            _uut.Update(_FakeFlightHandler);

            Assert.IsNotEmpty(_uut.GetConflicts());
        }

        [Test]
        public void Update_ListWithConflicts_NotifyWasCalled()
        {
            _FakeFlightHandler.GetFlights().Returns(testFlightsWithConflicts);

            _uut.Update(_FakeFlightHandler);

            _uut.Received().Notify(_uut);
        }

        [Test]
        public void Update_ListWithNoConflicts_NotifyWasCalled()
        {
            _FakeFlightHandler.GetFlights().Returns(testFlightsNoConflicts);

            _uut.Update(_FakeFlightHandler);

            _uut.Received().Notify(_uut);
        }

        [Test]
        public void Update_EmptyList_NotifyWasCalled()
        {
            _FakeFlightHandler.GetFlights().Returns(EmptyListFlights);

            _uut.Update(_FakeFlightHandler);

            _uut.Received().Notify(_uut);
        }
        #endregion
        
    }
}