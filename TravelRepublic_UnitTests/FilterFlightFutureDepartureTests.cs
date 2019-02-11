using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRepublic.FlightCodingTest;
using System.Collections.Generic;

namespace TravelRepublic.UnitTests
{
    [TestClass]
    public class FilterFlightFutureDepartureTests
    {
        private Flight flight;
        private FilterFlightFutureDeparture filter;
        
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

            filter = new FilterFlightFutureDeparture();
        }

        [TestMethod]
        public void IfSimpleOneSegmentFlight_ThenPasses_DepartureAfterCurrentDateFilter()
        {
            // Arrange

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfSimpleTwoSegmentFlight_ThenPasses_FutureDepartureFilter()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(3),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(5)
            };
            flight.Segments.Add(segment);

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfFlightDepartsBeforeCurrentDate_ThenFails_FutureDepartureFilter()
        {
            // Arrange
            flight.Segments[0].DepartureDate = DateTime.Now.AddDays(-1);

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsFalse(pass);
        }
    }
}
