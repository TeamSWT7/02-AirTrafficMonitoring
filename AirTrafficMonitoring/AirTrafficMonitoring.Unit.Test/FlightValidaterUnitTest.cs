using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class FlightValidaterUnitTest
    {

        private ITransponderReceiver _receiver;
        private FlightTransponderHandler _uut;
        private RawTransponderDataEventArgs _testEventArgs;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<FlightValidator>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _fakeFlightHandler = Substitute.For<FlightHandler>(_reciver);
        }


        [test]
        public void
        {
            _fakeFlightHandler
    }

    }
}
