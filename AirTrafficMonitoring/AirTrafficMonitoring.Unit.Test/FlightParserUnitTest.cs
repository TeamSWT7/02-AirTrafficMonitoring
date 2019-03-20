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
        public void Update_InfiniteList_ThrowsAtMax()
        {
            // Replace the return value of GetNext to return valid string
            // forever. The update while loop should hit max and throw exception
            _fakeFlightTransponderHandler.GetNext().Returns<string>(
                x => "ATR423;39045;12932;14000;20151006213456789"
            );

            Assert.Throws<Exception>(() => _uut.Update(_fakeFlightTransponderHandler));
        }

        [Test]
        public void FlightParser_ValidFlightString_FlightGetsAdded()
        {
            _uut.Update(_fakeFlightTransponderHandler);

            Flight flight = _uut.GetNext();

            Assert.IsNotNull(flight);
        }

        [Test]
        public void ParseString_ValidValue_TagIsCorrect()
        {
            _uut.ParseString("ATR423;39045;12932;14000;20151006213456789");

            Flight flight = _uut.GetNext();

            Assert.AreEqual(flight.tag, "ATR423");
        }

        [Test]
        public void ParseString_ValidValue_PositionIsCorrect()
        {
            _uut.ParseString("ATR423;39045;12932;14000;20151006213456789");

            Flight flight = _uut.GetNext();

            Assert.AreEqual(flight.position, new Coords(39045, 12932, 14000));
        }

        [Test]
        public void ParseString_InvalidLength_GetNextReturnsNull()
        {
            // Parsing string without timestamp
            _uut.ParseString("ATR423;39045;12932;14000");

            Assert.IsNull(_uut.GetNext());
        }

        [Test]
        public void ParseString_InvalidTimestamp_GetNextReturnsNull()
        {
            // Parsing string with invalid timestamp (missing ms)
            _uut.ParseString("ATR423;39045;12932;14000;20151006213456");

            Assert.IsNull(_uut.GetNext());
        }

        [Test]
        public void ParseDate_ValidValue_ReturnsCorrect()
        {
            DateTime correct = new DateTime(2015, 10, 06, 21, 34, 56, 789);

            Assert.AreEqual(_uut.ParseDate("20151006213456789"), correct);
        }

        [Test]
        public void ParseDate_InvalidValue_ReturnsNull()
        {
            Assert.IsNull(_uut.ParseDate("20151006213456"));
        }

        [Test]
        public void GetNext_SingleValue_ReturnsNullAtEnd()
        {
            // Call one to empty the testdata list
            _uut.GetNext();

            // Since the list is empty, GetNext() should return null.
            Assert.IsNull(_uut.GetNext());
        }
    }
}