using System;
using System.Collections.Generic;

namespace TravelRepublic.FlightCodingTest
{
    public class FilterBuilder
    {
        private Dictionary<string,FilterSet> DefaultFilters;

        public FilterBuilder()
        {
            DefaultFilters = new Dictionary<string,FilterSet>();

            // Setup basic default filter with check departure time is in the future
            // and total ground time is less than 2 hours (120 mins)
            var basicFilters = new FilterSet();
            basicFilters.AddFilter(new FilterFlightFutureDeparture());
            basicFilters.AddFilter(new FilterFlightMaximumGroundTime(120));

            DefaultFilters.Add("Basic", basicFilters);
        }

        public FilterSet GetFilterSet(string Name)
        {
            if (DefaultFilters.ContainsKey(Name)) {
                return DefaultFilters[Name];
            } else
            {
                throw new ArgumentException($"Default Filter with name {Name} not found", "Name");
            }
        }
    }
}
