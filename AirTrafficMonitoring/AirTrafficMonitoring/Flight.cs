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

        public override string ToString()
        {
            return $"{x,5}, {y,5}, {z,5}";
        }
    }
    
    public class Flight
    {
        public string tag;
        public Coords position;
        public DateTime timestamp;
        public double velocity;
        public double direction;

        public override string ToString()
        {
            return $"{tag} [{position}] with speed: {velocity:N2} m/s and angle: {direction:N2} deg";
        }
    }
}