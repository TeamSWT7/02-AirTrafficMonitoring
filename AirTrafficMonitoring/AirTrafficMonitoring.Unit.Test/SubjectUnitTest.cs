using AirTrafficMonitoring.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    class SubjectUnitTest
    {
        private Subject<IFlightParser> _uut;
        private IObserver<IFlightParser> _fakeObserver;

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<Subject<IFlightParser>>();
            _fakeObserver = Substitute.For<IObserver<IFlightParser>>();
        }

        [Test]
        public void Attach_ValidObserver_IsAddedToList()
        {
            _uut.Attach(_fakeObserver);

            Assert.AreEqual(_uut._observers.Count, 1);
        }

        [Test]
        public void Attach_ValidObserver_UpdateIsCalled()
        {
            var _fakeObserverInstance = Substitute.For<IFlightParser>();

            _uut.Attach(_fakeObserver);

            _uut.Notify(_fakeObserverInstance);

            _fakeObserver.Received().Update(_fakeObserverInstance);
        }

        [Test]
        public void Detach_ValidObserver_IsRemovedFromList()
        {
            _uut.Attach(_fakeObserver);
            _uut.Detach(_fakeObserver);

            Assert.AreEqual(_uut._observers.Count, 0);
        }

        [Test]
        public void Detach_ValidObserver_UpdateIsNotCalled()
        {
            var _fakeObserverInstance = Substitute.For<IFlightParser>();

            _uut.Attach(_fakeObserver);
            _uut.Detach(_fakeObserver);

            _uut.Notify(_fakeObserverInstance);

            _fakeObserver.DidNotReceive().Update(_fakeObserverInstance);
        }
    }
}
