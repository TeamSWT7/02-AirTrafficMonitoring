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
        private ConflictHandler _uut;
        private IFlightValidator _FakeFlightValidator;
        private IFlightHandler _FakeFlightHandler;
        private Flight _flight1;
        private Flight _flight2;

        [SetUp]
        public void Setup()
        {
            // Create fakes
            _FakeFlightHandler = Substitute.For<IFlightHandler>();
            _FakeFlightValidator = Substitute.For<IFlightValidator>();

            _uut = Substitute.For<ConflictHandler>();
        }

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
    }
}