using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace AirTrafficMonitoring
{
    public class Airspace
    {
        public Coords Position { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }

        public bool InArea(Flight flight)
        {
            return ((flight.position.x >= Position.x && flight.position.x < Position.x + Width)
                && (flight.position.y >= Position.y && flight.position.y < Position.y + Length)
                && (flight.position.z >= Position.z && flight.position.z < Position.z + Height));
        }
    }
}