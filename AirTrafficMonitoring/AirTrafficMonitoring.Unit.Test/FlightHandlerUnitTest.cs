﻿using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            //Create attributes for tests
            _fakeFlightParser = Substitute.For<IFlightParser>();
            _uut = Substitute.For<FlightHandler>();
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
            Assert.AreEqual(279.46,_uut.CalculateDirection(_flight1,_flight2),0.1);
        }
        #endregion
    }
}