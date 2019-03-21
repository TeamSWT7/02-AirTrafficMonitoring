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
        private List<Flight> _multipleFlights;

        [SetUp]
        public void Setup()
        {
            //Create attributes for tests
            _fakeFlightParser = Substitute.For<IFlightParser>();

            _uut = Substitute.For<FlightHandler>();

            _multipleFlights = new List<Flight>
            {
                new Flight()
                {
                    tag = "ATR423",
                    position = new Coords(39045, 12932, 14000),
                },
                new Flight()
                {
                    tag = "BCD123",
                    position = new Coords(10005, 85890, 12000),
                },
                new Flight()
                {
                    tag = "XYZ987",
                    position = new Coords(25059, 75654, 4000),
                }
            };
        }
        #endregion

        [Test]
        public void Update_MultipleValues_ListIsCorrect()
        {
            _fakeFlightParser.GetNext().Returns(
                x => _multipleFlights[0],
                x => _multipleFlights[1],
                x => _multipleFlights[2],
                x => null
            );

            _uut.Update(_fakeFlightParser);

            Assert.AreEqual(_uut.GetFlights(), _multipleFlights);
        }

        [Test]
        public void Update_EmptyValues_ListIsEmpty()
        {
            _fakeFlightParser.GetNext().Returns(
                x => null
            );

            _uut.Update(_fakeFlightParser);

            Assert.AreEqual(_uut.GetFlights().Count, 0);
        }

        #region CalculateDirection
        [Test]
        public void CalculateDirection_TwoValues_DirectionIsCorret()
        {
            Flight flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000)
            };
            Flight flight2 = new Flight()
            {
                position = new Coords(1200, 800, 3000)
            };

            Assert.AreEqual(279.46, _uut.CalculateDirection(flight1, flight2), 0.01);
        }
        [Test]
        public void CalculateDirection_FlightsSameYCoordinate_DirectionIsCorrect()
        {
            Flight flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000)
            };
            Flight flight2 = new Flight()
            {
                position = new Coords(1500, 2000, 3000)
            };

            Assert.AreEqual(0, _uut.CalculateDirection(flight1, flight2));
        }

        #endregion

        #region CalculateVelocity

        [Test]
        public void CalculateVelocity_NewUpdateSimpleChange_ResultIsCorrect()
        {
            Flight flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            Flight flight2 = new Flight()
            {
                position = new Coords(2000, 2000, 3000),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 5)
            };

            Assert.AreEqual(200, _uut.CalculateVelocity(flight1, flight2));
        }

        [Test]
        public void CalculateVelocity_NewUpdateCompleteChange_ResultIsCorrect()
        {
            Flight flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            Flight flight2 = new Flight()
            {
                position = new Coords(800, 2400, 4500),
                timestamp = new DateTime(2019, 03, 21, 18, 10, 10)
            };

            Assert.AreEqual(156.52, _uut.CalculateVelocity( flight1, flight2), 0.01);
        }

        #endregion

        #region UpdateFlightInfo
        [Test]
        public void UpdateFlightInfo_NewUpdate_ResultIsCorrect()
        {
            Flight flight1 = new Flight()
            {
                position = new Coords(1000, 2000, 3000),
                tag = "AT819",
                timestamp = new DateTime(2019, 03, 21, 18, 10, 0)
            };
            Flight flight2 = new Flight()
            {
                position = new Coords(800, 2400, 4500),
                tag = "AT819",
                timestamp = new DateTime(2019, 03, 21, 18, 10, 10)
            };
            _uut.UpdateFlightInfo(flight1, flight2);
            flight2.direction = flight1.direction;
            flight2.velocity = flight1.velocity;
            Assert.AreSame(flight2, flight1);
        }

        #endregion
    }
}