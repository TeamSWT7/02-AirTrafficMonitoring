using System;

namespace AirTrafficMonitoring
{   
    public class Flight
    {
        public string tag;
        public Coords position;
        public DateTime timestamp;
        public double velocity;
        public double direction;

        public override bool Equals(object obj)
        {
            if (!(obj is Flight other))
                return false;

            return (position.Equals(other.position)
                    && Math.Abs(direction - other.direction) < 0.1 // Added tolerance to ensure precision
                    && Math.Abs(velocity - other.velocity) < 0.1
                    && tag == other.tag
                    && timestamp == other.timestamp);
        }

        public override string ToString()
        {
            return $"{tag} [{position}] with speed: {velocity:N2} m/s and angle: {direction:N2} deg";
        }
    }
}