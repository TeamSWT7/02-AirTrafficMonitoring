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
        private Flight flight1 = new Flight();
        private Flight flight2 = new Flight();

        [SetUp]
        public void Setup()
        {
            _FakeFlightHandler = Substitute.For<IFlightHandler>();
            _FakeFlightValidator = Substitute.For<IFlightValidator>();
            _uut = Substitute.For<ConflictHandler>(_FakeFlightValidator);
            flight1.position.x = 15500;
            flight1.position.y = 15500;
            flight1.position.z = 16000;

            flight2.position.x = 15600;
            flight2.position.y = 15450;
            flight2.position.z = 15900;
        }

        public void ReceiveTestEvent()
        {
            List<Flight> testData = new List<Flight>()
            {
                
            };
        }

        [Test]
        public void TestFunction_CheckVerticalDistance()
        {
            Assert.That(_uut.CheckVerticalDistance(flight1, flight2), Is.EqualTo(true));
        }
    }
}