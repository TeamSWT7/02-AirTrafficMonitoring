<h1 align="center">Air Traffic Monitoring System</h1>

<div align="center">

A system, which can monitor and present the air traffic in a given region and
detect interesting situations in the air traffic.

</div>

FlightTransponderHandler.SetSubscriber(FlightParser);
FlightParser.SetSubscriber(FlightValidator)
FlightValidator.SetSubscriber(FlightHandler)
FlightHandler.SetSubscriber(ConflicHandler)


ISubject {
    --
    + Observers : List<Observer>
}
