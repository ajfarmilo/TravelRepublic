using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelRepublic.FlightCodingTest
{
    public class Flight
    {
        public IList<Segment> Segments { get; set; }

        // Extra functions added to the original class, could add to a new sub-class instead if
        // the Flight class was in a 3rd party library
        public Boolean IsValid()
        {
            var valid = true;

            // If any Segment Arrives before it Departs then the Flight is not valid
            valid = Segments.Where(s => s.ArrivalDate < s.DepartureDate).Count() == 0;

            // Could add other checks here like whether a Segment departs before the previous one arrives

            return valid;
        }

        // Could deduce this once and store against the Flight if regularly used
        public DateTime DepartureDateTime()
        {
            return Segments.FirstOrDefault().DepartureDate;
        }

        // Could calculate this once and store against the Flight if regularly used
        public Int16 TotalGroundTimeMinutes()
        {
            Int16 total = 0;

            // Add together the total minutes between Segments (where +ve because that is not yet guaranteed)
            for (var i = 0; i < Segments.Count() - 1; i++)
            {
                TimeSpan span = Segments[i + 1].DepartureDate.Subtract(Segments[i].ArrivalDate);
                if (span.TotalMinutes > 0) total += Convert.ToInt16(span.TotalMinutes);
            }

            return total;
        }

        // Could deduce this once and store against the Flight if regularly used
        public String DisplayText()
        {
            var text = new StringBuilder();
            foreach (Segment segment in Segments)
            {
                if (text.Length > 0) text.Append("; ");
                // Date format could be localized/provided in local time for the destination(s)
                text.Append(segment.DepartureDate.ToString("dd-MMM-yy HH:mm"));
                text.Append(" to ");
                text.Append(segment.ArrivalDate.ToString("dd-MMM-yy HH:mm"));
            }

            return text.ToString();
        }
    }
}
