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
        private FlightHandler _uut;
        private IFlightTransponderHandler _fakeFlightTransponderHandler;
        private Flight _flight1;
        private Flight _flight2;

        [SetUp]
        public void Setup()
        {
            //Create attributes for tests
            _fakeFlightTransponderHandler = Substitute.For<IFlightTransponderHandler>();
            _uut = Substitute.For<FlightHandler>();
        }
    }
}