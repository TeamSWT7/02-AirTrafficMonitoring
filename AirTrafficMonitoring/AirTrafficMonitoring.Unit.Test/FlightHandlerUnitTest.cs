﻿using System;
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

        #region Update
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

        #endregion

        #region UpdateFlightInfo
        [Test]
        public void UpdateFlightInfo_NewUpdate_ResultIsCorrect()
        {
            Flight prev = new Flight()
            {
                tag = "ATR423",
                position = new Coords(10005, 85890, 12000),
                timestamp = new DateTime(2019, 03, 22, 11, 15, 00),
            };
            Flight next = new Flight()
            {
                tag = "ATR423",
                position = new Coords(39045, 12932, 14000),
                timestamp = new DateTime(2019, 03, 22, 11, 20, 00),
            };
            Flight expected = new Flight()
            {
                tag = "ATR423",
                position = new Coords(39045, 12932, 14000),
                timestamp = new DateTime(2019, 03, 22, 11, 20, 00),
                velocity = 261.84,
                direction = 21.70,
            };

            _uut.UpdateFlightInfo(prev, next);

            Console.WriteLine(prev);
            Console.WriteLine(expected);
            Console.WriteLine(prev.Equals(expected));

            Assert.IsTrue(prev.Equals(expected));
        }

        #endregion

        #region UpdateList

        [Test]
        public void UpdateList_SameTagNewData_ListIsUpdated()
        {
            _fakeFlightParser.GetNext().Returns(
                x => _multipleFlights[0],
                x => _multipleFlights[1],
                x => _multipleFlights[2],
                x => null
            );

            _uut.Update(_fakeFlightParser);

            Flight next = new Flight()
            {
                tag = "ATR423",
                position = new Coords(4000, 20000, 5000),
                timestamp = new DateTime(2019,03,22,12,30,00),
                direction = 258.6
            };
            _uut.HandleFlight(next);

            Assert.AreEqual(next, _uut.GetFlights()[0]);
        }

        [Test]
        public void UpdateList_NewTag_ListIsUpdated()
        {
            _fakeFlightParser.GetNext().Returns(
                x => _multipleFlights[0],
                x => _multipleFlights[1],
                x => _multipleFlights[2],
                x => null
            );

            _uut.Update(_fakeFlightParser);

            Flight next = new Flight()
            {
                tag = "LNH385",
                position = new Coords(4000, 20000, 5000),
                timestamp = new DateTime(2019, 03, 22, 12, 30, 00),
                direction = 168.6
            };
            _uut.HandleFlight(next);

            Assert.AreEqual(next,_uut.GetFlights()[3]);
        }

        #endregion

        #region CalculateDirection
        [Test]
        public void CalculateDirection_TwoValues_DirectionIsCorrect()
        {
            Flight prev = new Flight()
            {
                position = new Coords(1000, 2000, 3000)
            };
            Flight next = new Flight()
            {
                position = new Coords(1200, 800, 3000)
            };

            Assert.AreEqual(9.462, _uut.CalculateDirection(prev, next), 0.001);
        }
        [Test]
        public void CalculateDirection_SameYCoordinate_DirectionIsCorrect()
        {
            Flight prev = new Flight()
            {
                position = new Coords(1000, 2000, 3000)
            };
            Flight next = new Flight()
            {
                position = new Coords(1500, 2000, 3000)
            };

            Assert.AreEqual(90, _uut.CalculateDirection(prev, next));
        }

        #endregion

        #region CalculateVelocity

        [Test]
        public void CalculateVelocity_XCoordinateChanges_ResultIsCorrect()
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
        public void CalculateVelocity_AllAxisChanges_ResultIsCorrect()
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

            Assert.AreEqual(156.524, _uut.CalculateVelocity( flight1, flight2), 0.001);
        }

        #endregion
    }
}