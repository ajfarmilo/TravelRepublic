using System.Collections.Generic;

namespace TravelRepublic.FlightCodingTest
{
    public class FilterSet
    {
        private List<IFilter> Filters;

        public FilterSet()
        {
            Filters = new List<IFilter>();
        }

        public void AddFilter(IFilter additionalFilter)
        {
            Filters.Add(additionalFilter);
        }

        public bool PassesFilters(Flight flight)
        {
            foreach (IFilter filter in Filters)
            {
                if (!filter.PassesFilter(flight))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
