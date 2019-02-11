using System;

namespace TravelRepublic.FlightCodingTest
{
    public class FilterFlightFutureDeparture : IFilter
    {
        public bool PassesFilter(Flight flight)
        {
            // Fail filter if Departure is before current date/time
            return !(flight.DepartureDateTime() < DateTime.Now);
        } 
    }
}
