using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class AirTrafficMonitorUnitTest
    {
        private AirTrafficMonitor _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new AirTrafficMonitor(Substitute.For<ITransponderReceiver>());
        }

        [Test]
        public void SetPrinter_ValidPrinter_PrinterIsSet()
        {
            IPrinter printer = Substitute.For<IPrinter>();

            _uut.SetPrinter(printer);

            Assert.AreEqual(_uut._printer, printer);
        }
    }
}