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

        public override string ToString()
        {
            return $"{_first.tag} ({_first.position}) conflicts with {_second.tag} ({_second.position}), Time of occurrence: {_first.timestamp}";
        }
    }
}