using System;

namespace TravelRepublic.FlightCodingTest
{
    public class FilterFlightMaximumGroundTime : IFilter
    {
        private short MaxTotalGroundTimeMinutes { get; set; }

        public FilterFlightMaximumGroundTime (short maxTotalGroundTimeMins)
        {
            // Ground Time under 0 mins doesn't make sense
            if (maxTotalGroundTimeMins >= 0)
            {
                MaxTotalGroundTimeMinutes = maxTotalGroundTimeMins;
            } else
            {
                throw new ArgumentException("Maximum Ground Time cannot be negative", "maxTotalGroundTimeMins");
            }
        }

        public bool PassesFilter(Flight flight)
        {
            // Fail filter if Total Ground Mins greater than desired limit
            return !(flight.TotalGroundTimeMinutes() > MaxTotalGroundTimeMinutes);
        } 
    }
}
