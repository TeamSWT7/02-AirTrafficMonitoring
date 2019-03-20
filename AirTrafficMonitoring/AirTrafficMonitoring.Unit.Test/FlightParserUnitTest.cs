using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class FlightParserUnitTest
    {
        private FlightParser _uut;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<FlightParser>();
        }

        [Test]
        public void Test_()
        {
            
        }
    }
}