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
    public class FlightTransponderHandlerUnitTest
    {
        private ITransponderReceiver _receiver;
        private FlightTransponderHandler _uut;

        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();

            _uut = Substitute.For<FlightTransponderHandler>(_receiver);
        }

        [Test]
        public void TestReception()
        {
            List<string> testData = new List<string>()
            {
                "ATR423;39045;12932;14000;20151006213456789",
                "BCD123;10005;85890;12000;20151006213456789",
                "XYZ987;25059;75654;4000;20151006213456789",
            };

            var eventArgs = new RawTransponderDataEventArgs(testData);

            _receiver.TransponderDataReady
                += Raise.EventWith(eventArgs);
            
            _uut.Received().OnReceivedData(null, eventArgs);
        }
    }
}