using System.Collections.Generic;
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
        private RawTransponderDataEventArgs _testEventArgs;

        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();

            _uut = Substitute.For<FlightTransponderHandler>(_receiver);

            // Setup testdata
            List<string> testData = new List<string>()
            {
                "ATR423;39045;12932;14000;20151006213456789",
                "BCD123;10005;85890;12000;20151006213456789",
                "XYZ987;25059;75654;4000;20151006213456789",
            };

            _testEventArgs = new RawTransponderDataEventArgs(testData);

            _receiver.TransponderDataReady
                += Raise.EventWith(_testEventArgs);
        }

        [Test]
        public void OnReceivedData_RaisedTestEvent_AttachedFunctionCalled()
        {
            _uut.Received().OnReceivedData(null, _testEventArgs);
        }

        [Test]
        public void TransponderEvent_RaisedEvent_NotifyWasCalled()
        {
            _uut.Received().Notify(_uut);
        }

        [Test]
        public void GetNext_MultipleStrings_FirstReturnsCorrect()
        {
            Assert.AreEqual(_uut.GetNext(), "ATR423;39045;12932;14000;20151006213456789");
        }

        [Test]
        public void GetNext_MultipleStrings_SecondReturnsCorrect()
        {
            _uut.GetNext();

            Assert.AreEqual(_uut.GetNext(), "BCD123;10005;85890;12000;20151006213456789");
        }

        [Test]
        public void GetNext_MultipleStrings_ReturnsNullAtEnd()
        {
            // Repeat 3 times to empty the testdata list
            _uut.GetNext();
            _uut.GetNext();
            _uut.GetNext();
            
            // Since the list is empty, GetNext() should return null.
            Assert.IsNull(_uut.GetNext());
        }
    }
}