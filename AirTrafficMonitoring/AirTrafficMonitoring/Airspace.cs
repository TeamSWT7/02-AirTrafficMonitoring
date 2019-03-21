using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace AirTrafficMonitoring
{
    public class Airspace
    {
        public Coords Position { get; private set; }
        public int Length { get; private set; }
        public int Width { get; private set; }
        public int MinHeight { get; private set; }
        public int MaxHeight { get; private set; }
        public int AirspaceHeight { get; private set; }

        public Airspace(int width = 80000, int length = 80000, int minZ = 500, int airspaceHeight = 20000)
        {
            Position.x = Width;
            Width = width;

            Position.y = Length;
            Length = length;

            Position.z = minZ;

            AirspaceHeight = airspaceHeight;
        }

        public bool InArea(Flight flight)
        {
            if (flight.position.x > Position.x + Width || flight.position.x < Position.x)
            {
                return true;
            }
            else if (flight.position.y > Position.y + Length || flight.position.y < Position.y)
            {
                return true;
            }
            else if (flight.position.z > Position.z + AirspaceHeight || flight.position.z < Position.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}