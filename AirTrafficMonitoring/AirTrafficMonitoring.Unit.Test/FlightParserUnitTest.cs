using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class FlightParserUnitTest
    {
        private ITransponderReceiver _receiver;
        private FlightTransponderHandler _fakeFlightTransponderHandler;
        private FlightParser _uut;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<FlightParser>();

            _receiver = Substitute.For<ITransponderReceiver>();

            _fakeFlightTransponderHandler = Substitute.For<FlightTransponderHandler>(_receiver);
            // _fakeFlightTransponderHandler.GetNext().Returns("ATR423;39045;12932;14000;20151006213456789");

            _fakeFlightTransponderHandler.Attach(_uut);
        }

        [Test]
        public void FlightParser_OnSubjectNotify_UpdateWasCalled()
        {
            _fakeFlightTransponderHandler.Notify(_fakeFlightTransponderHandler);

            _uut.Received().Update(_fakeFlightTransponderHandler);
        }
    }
}