using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelRepublic.FlightCodingTest;
using System.Collections.Generic;

namespace TravelRepublic.UnitTests
{
    [TestClass]
    public class FilterBuilderTests
    {
        private FilterBuilder builder;
        
        [TestInitialize]
        public void SimpleFlightAndFilter()
        {
            builder = new FilterBuilder();
        }

        [TestMethod]
        public void IfBasicFilterIsInDefaults_ThenNoException()
        {
            builder.GetFilterSet("Basic");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "name")]
        public void IfUnknownFilterIsRequested_ThenException()
        {
            builder.GetFilterSet("Unknown");
        }
    }
}
