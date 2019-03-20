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
            this.y = z;
        }
    }
    
    public class Flight
    {
        public string tag;
        public Coords position;
        public DateTime timestamp;
        public string altitude;
        public double velocity;
        public double direction;
    }
}