using System;

namespace TravelRepublic.FlightCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {

            // Get Flights
            var flightBuilder = new FlightBuilder();
            var flightList = new ResultSet(flightBuilder.GetFlights());

            // Display List of Flights - not normally needed, for comparison with Filtered list
            Console.WriteLine("~ Initial Flight List ~");
            flightList.WriteToConsole();

            // Could ask User for filters like Departure Date here

            // Build Filters
            var filterBuilder = new FilterBuilder();

            // Was intending to use a new prototype interface to clone the Basic default
            // filter but ran out of time
            
            // Filter Flights
            flightList.ApplyFilters(filterBuilder.GetFilterSet("Basic"));

            // Display Final List of Flights
            Console.WriteLine("~ Filter Results ~");
            flightList.WriteToConsole();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}