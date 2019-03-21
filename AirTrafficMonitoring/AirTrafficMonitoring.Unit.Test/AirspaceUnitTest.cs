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
    public class AirspaceUnitTest
    {
        private Airspace _uut;

        [SetUp]
        public void init()
        {
            _uut = Substitute.For<Airspace>();
            
            _uut.Position = new Coords(500, 500, 500);

            _uut.Width = 30000;
            _uut.Length = 20000;
            _uut.Height = 10000;
        }

        [TestCase(6000, 6000, 10000)]
        [TestCase(6000, 20500, 10000)] // Edge case
        public void InArea_ValidValues_ReturnsTrue(int x, int y, int z)
        {
            // Create flight from testcase values
            Flight flight = new Flight
            {
                position = new Coords(x, y, z),
            };

            Assert.IsTrue(_uut.InArea(flight));
        }

        [TestCase(400, 10000, 5000)] // Test x
        [TestCase(15000, 20501, 5000)] // Test y
        [TestCase(15000, 10000, 12000)] // Test z
        public void InArea_InvalidValues_ReturnsFalse(int x, int y, int z)
        {
            // Create flight frmo testcase values
            Flight flight = new Flight
            {
                position = new Coords(x, y, z),
            };

            Assert.IsFalse(_uut.InArea(flight));
        }

    }
}