using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRepublic.FlightCodingTest;
using System.Collections.Generic;

namespace TravelRepublic.UnitTests
{
    [TestClass]
    public class FilterTests
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
        }

        [TestMethod]
        public void IfSimpleOneSegmentFlight_ThenPasses_DepartureAfterCurrentDateFilter()
        {
            // Arrange
            filters.AddFilter(new FilterFlightFutureDeparture());

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfSimpleOneSegmentFlight_ThenPasses_MaxGroundTimeCurrentFilter()
        {
            // Arrange
            filters.AddFilter(new FilterFlightMaximumGroundTime(120));

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfSimpleTwoSegmentFlight_ThenPassesFilter()
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
        public void IfFlightDepartsOnDepartureLimit_ThenPassesFilter()
        {
            // Arrange
            //filters.EarliestDepartureDateTime = flight.Segments[0].DepartureDate;

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfFlightDepartsBeforeDepartureLimit_ThenFailsFilter()
        {
            // Arrange
            //filters.EarliestDepartureDateTime = DateTime.Now.AddDays(2);

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void IfTwoSegmentFlightGroundTimeMatchesLimit_ThenPassesFilter()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(12),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(14)
            };
            flight.Segments.Add(segment);

            // Act
            bool pass = filters.PassesFilters(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfTwoSegmentFlightGroundTimeOverLimit_ThenFailsFilter()
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
