using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRepublic.FlightCodingTest;
using System.Collections.Generic;

namespace TravelRepublic.UnitTests
{
    [TestClass]
    public class FilterSetTests
    {
        private Flight flight;
        private FilterSet filters;
        
        [TestInitialize]
        public void SimpleFlightAndFilter()
        {
            flight = new Flight()
            {
                Segments = new List<Segment>()
            };
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(2)
            };
            flight.Segments.Add(segment);

            filters = new FilterSet();
            filters.AddFilter(new FilterFlightFutureDeparture());
            filters.AddFilter(new FilterFlightMaximumGroundTime(120));
        }

        [TestMethod]
        public void IfSimpleTwoSegmentFlight_ThenPasses_WholeFilterSet()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(3),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(5)
            };
            flight.Segments.Add(segment);

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfTwoSegmentFlightDepartsInPast_ThenFails_WholeFilterSet()
        {
            // Arrange
            flight.Segments[0].DepartureDate = DateTime.Now.AddDays(-1);
            flight.Segments[0].ArrivalDate = DateTime.Now.AddDays(-1).AddHours(2);

            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(-1).AddHours(3),
                ArrivalDate = DateTime.Now.AddDays(-1).AddHours(5)
            };
            flight.Segments.Add(segment);

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void IfTwoSegmentFlightHasLargeGroundTime_ThenFails_WholeFilterSet()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(13),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(15)
            };
            flight.Segments.Add(segment);

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsFalse(pass);
        }
    }
}
