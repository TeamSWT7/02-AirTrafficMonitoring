using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class FlightValidatorUnitTest
    {
        private FlightValidator _uut;
        private Flight _flight;
        private Airspace _airspace;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<FlightValidator>();
        }

        [Test]
        public void Validate_Flight_inArea_airspace()
        {
            _flight = new Flight
            {
                position = new Coords(16000, 16000, 15000),
            };

         Assert.That(_airspace.InArea());
           
        }
    }
}