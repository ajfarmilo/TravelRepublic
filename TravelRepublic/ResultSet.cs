using System;
using System.Collections.Generic;

namespace TravelRepublic.FlightCodingTest
{
    public class ResultSet
    {
        // Was intending to make this more generic/not flight specific but use a new
        // interface but ran out of time
        private IList<Flight> Results;

        public ResultSet(IList<Flight> results)
        {
            Results = results;
        }

        public void ApplyFilters(FilterSet filters)
        {
            for (int i = Results.Count - 1; i >= 0; i--)
            {
                // if Flight is invalid or fails Filters then remove from result set
                if ((!Results[i].IsValid()) || (!filters.PassesFilters(Results[i])))
                {
                    Results.RemoveAt(i);
                }
            }
        }

        public void WriteToConsole()
        {
            Console.WriteLine($"Result count: {Results.Count}");
            foreach (Flight flight in Results)
            {
                Console.WriteLine(flight.DisplayText());
            }
            Console.WriteLine();
        }
    }
}
