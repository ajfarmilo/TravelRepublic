using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRepublic.FlightCodingTest;
using System.Collections.Generic;

namespace TravelRepublic.UnitTests
{
    [TestClass]
    public class FlightTests
    {
        private Flight flight;

        [TestInitialize]
        public void SimpleFlight()
        {
            flight = new Flight()
            {
                Segments = new List<Segment>()
            };
            Segment segment = new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(1)
            };
            flight.Segments.Add(segment);
        }

        [TestMethod]
        public void IfSimpleOneSegmentFlight_ThenIsValid()
        {
            // Arrange
            
            // Act
            Boolean valid = flight.IsValid();

            // Assert
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void IfOneSegmentFlightArrivesOnDeparture_ThenIsValid()
        {
            // Arrange
            flight.Segments[0].ArrivalDate = flight.Segments[0].DepartureDate;

            // Act
            Boolean valid = flight.IsValid();

            // Assert
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void IfOneSegmentFlightArrivesBeforeDeparture_ThenNotValid()
        {
            // Arrange
            flight.Segments[0].ArrivalDate = flight.Segments[0].DepartureDate.AddHours(-1);

            // Act
            Boolean valid = flight.IsValid();

            // Assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void IfBothSegmentsOfFlightArriveAfterDeparture_ThenIsValid()
        {
            // Arrange
            flight.Segments.Add(new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(2),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(3)
            });

            // Act
            Boolean valid = flight.IsValid();

            // Assert
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void IfSecondSegmentOfFlightArrivesBeforeDeparture_ThenNotValid()
        {
            // Arrange
            flight.Segments.Add(new Segment()
            {
                DepartureDate = DateTime.Now.AddDays(1).AddHours(2),
                ArrivalDate = DateTime.Now.AddDays(1).AddHours(1)
            });

            // Act
            Boolean valid = flight.IsValid();

            // Assert
            Assert.IsFalse(valid);
        }
    }
}
