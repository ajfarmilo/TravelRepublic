using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRepublic.FlightCodingTest;
using System.Collections.Generic;

namespace TravelRepublic.UnitTests
{
    [TestClass]
    public class FilterFlightMaximumGroundTimeTests
    {
        private Flight flight;
        private FilterFlightMaximumGroundTime filter;
        
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
        }

        [TestMethod]
        public void IfSimpleOneSegmentFlight_ThenPasses_MaxGroundTimeFilter()
        {
            // Arrange
            filter = new FilterFlightMaximumGroundTime(600);

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfSimpleTwoSegmentFlight_ThenPasses_MaxGroundTimeFilter()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(3),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(5)
            };
            flight.Segments.Add(segment);

            filter = new FilterFlightMaximumGroundTime(600);

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfTwoSegmentFlightGroundTimeMatchesLimit_ThenPasses_MaxGroundTimeFilter()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(12),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(14)
            };
            flight.Segments.Add(segment);

            filter = new FilterFlightMaximumGroundTime(600);

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void IfTwoSegmentFlightGroundTimeOverLimit_ThenFails_MaxGroundTimeFilter()
        {
            // Arrange
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(13),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(15)
            };
            flight.Segments.Add(segment);

            filter = new FilterFlightMaximumGroundTime(600);

            // Act
            bool pass = filter.PassesFilter(flight);

            // Assert
            Assert.IsFalse(pass);
        }

        [TestMethod]
        public void IfMaxGroundTimeFilterCreatedWithZeroMins_ThenNoException()
        {
            // Arrange

            // Act
            filter = new FilterFlightMaximumGroundTime(0);

            // Assert
            Assert.IsNotNull(filter);
        }

        [TestMethod]
        public void IfMaxGroundTimeFilterCreatedWithSensibleMins_ThenNoException()
        {
            filter = new FilterFlightMaximumGroundTime(120);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "maxTotalGroundTimeMins")]
        public void IfMaxGroundTimeFilterCreatedWithNegativeMins_ThenArgumentException()
        {
            filter = new FilterFlightMaximumGroundTime(-1);
        }
    }
}
