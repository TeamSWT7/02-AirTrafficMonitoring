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
    public class FlightValidatorUnitTest
    {
        private FlightValidator _uut;
        private Flight _flight;
        private Airspace _airspace;

        [SetUp]
        public void Setup()
        {
            
            //crating fakes
            _uut = Substitute.For<FlightValidator>();


        }
    }
}