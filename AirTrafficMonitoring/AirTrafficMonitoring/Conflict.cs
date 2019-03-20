namespace AirTrafficMonitoring
{
    public class Conflict
    {
        private Flight _first;
        private Flight _second;

        public Conflict(Flight first, Flight second)
        {
            _first = first;
            _second = second;
        }
    }
}