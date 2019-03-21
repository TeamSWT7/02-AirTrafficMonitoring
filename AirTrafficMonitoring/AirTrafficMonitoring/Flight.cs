using System;

namespace AirTrafficMonitoring
{
    public class Coords {
        public int x;
        public int y;
        public int z;

        public Coords(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {

            if (!(obj is Coords other))
                return false;

            return (x == other.x && y == other.y && z == other.z);
        }
    }
    
    public class Flight
    {
        public string tag;
        public Coords position;
        public DateTime timestamp;
        public double velocity;
        public double direction;
    }
}