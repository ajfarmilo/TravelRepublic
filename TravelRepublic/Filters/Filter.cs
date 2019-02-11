using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRepublic.FlightCodingTest
{
    public interface IFilter
    {
        bool PassesFilter(Flight flight);
    }
}
