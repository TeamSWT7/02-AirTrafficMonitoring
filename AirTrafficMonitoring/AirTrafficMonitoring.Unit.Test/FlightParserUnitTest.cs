using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using AirTrafficMonitoring.Interfaces;
using NSubstitute.ReturnsExtensions;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class FlightParserUnitTest
    {
        private IFlightTransponderHandler _fakeFlightTransponderHandler;
        private FlightParser _uut;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<FlightParser>();

            _fakeFlightTransponderHandler = Substitute.For<IFlightTransponderHandler>();

            // Set multiple return values to simulate the first item being removed from
            // the flightTransponderHandler.
            _fakeFlightTransponderHandler.GetNext().Returns<string>(
                x => "ATR423;39045;12932;14000;20151006213456789",
                x => null
            );
        }

        [Test]
        public void FlightParser_ValidFlightString_FlightGetsAdded()
        {
            _uut.Update(_fakeFlightTransponderHandler);

            Flight flight = _uut.GetNext();

            Assert.IsNotNull(flight);
        }

        [Test]
        public void FlightParser_ValidFlightString_TagIsCorrect()
        {
            _uut.Update(_fakeFlightTransponderHandler);

            Flight flight = _uut.GetNext();

            Assert.AreEqual(flight.tag, "ATR423");
        }
    }
}