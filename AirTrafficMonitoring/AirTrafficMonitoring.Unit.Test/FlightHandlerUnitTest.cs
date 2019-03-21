using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class FlightHandlerUnitTest
    {
        #region Setup
        private FlightHandler _uut;
        private IFlightParser _fakeFlightParser;
        private Flight _flight1;
        private Flight _flight2;
        private List<Flight> _flights1;
        private List<Flight> _flights2;

        [SetUp]
        public void Setup()
        {
            //Create attributes for tests
            _fakeFlightParser = Substitute.For<IFlightParser>();
            _uut = Substitute.For<FlightHandler>();
            _flights1 = new List<Flight>();
            _flights2 = new List<Flight>();
        }
        #endregion

        #region CalculateDirection
        [Test]
        public void CalculateDirection_NewUpdate_ResultIsCorrect()
        {
            _flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000)
            };
            _flight2 = new Flight()
            {
                position = new Coords(1200, 800, 3000)
            };
            Assert.AreEqual(279.46,_uut.CalculateDirection(_flight1,_flight2),0.01);
        }
        [Test]
        public void CalculateDirection_NewUpdateSameYCoordinate_ResultIsCorrect()
        {
            _flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000)
            };
            _flight2 = new Flight()
            {
                position = new Coords(1500, 2000, 3000)
            };
            Assert.AreEqual(0, _uut.CalculateDirection(_flight1,_flight2));
        }

        #endregion

        #region CalculateVelocity

        [Test]
        public void CalculateVelocity_NewUpdateSimpleChange_ResultIsCorrect()
        {
            _flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            _flight2 = new Flight()
            {
                position = new Coords(2000, 2000, 3000),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 5)
            };
            Assert.AreEqual(200, _uut.CalculateVelocity(_flight1,_flight2));
        }

        [Test]
        public void CalculateVelocity_NewUpdateCompleteChange_ResultIsCorrect()
        {
            _flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            _flight2 = new Flight()
            {
                position = new Coords(800, 2400, 4500),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 10)
            };
            Assert.AreEqual(156.52, _uut.CalculateVelocity(_flight1, _flight2),0.01);
        }

        #endregion
        /*
        #region GetFlights

        [Test]
        public void GetFlights_GetsTheList_TheListsAreIdentical()
        {
            _flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                tag = "AK153",
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            _flight2 = new Flight()
            {
                position = new Coords(2000, 2000, 3000),
                tag = "AQ164",
                timestamp = new DateTime(2019, 03, 21, 18, 10, 5)
            };
            _flights1.Add(_flight1);
            _flights1.Add(_flight2);
            _flights2.Add(_flight1);
            _flights2.Add(_flight2);
            CollectionAssert.AreEqual(_flights1, _uut.GetFlights().Returns(_flights2));
        }

        #endregion
        */
        #region UpdateFlightInfo
        [Test]
        public void UpdateFlightInfo_NewUpdate_ResultIsCorrect()
        {
            _flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                tag = "AT819",
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            _flight2 = new Flight()
            {
                position = new Coords(800, 2400, 4500),
                tag = "AT819",
                timestamp = new DateTime(2019, 03, 21, 18, 10, 10)
            };
            _uut.UpdateFlightInfo(_flight1,_flight2);
            _flight2.direction = _flight1.direction;
            _flight2.velocity = _flight1.velocity;
            Assert.AreSame(_flight2, _flight1);
        }

        #endregion
    }
}