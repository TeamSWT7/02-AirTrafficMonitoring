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
        }

        [TestCase(10, 10, 10, false)]
        [TestCase(6000, 6000, 10000, true)]
        public void InArea_Check(int width, int length, int height, bool result)
        {
            //creating flight
            Flight _flight = new Flight
            {
                position = new Coords(5000, 5000, 5000),
            };
            
            _uut.Position = new Coords(0, 0, 0);

            //Setting airspace properties to set values
            _uut.Width = width;
            _uut.Length = length;
            _uut.Height = height;


            
            
            Assert.AreEqual(result, _uut.InArea(_flight));

        }

    }
}