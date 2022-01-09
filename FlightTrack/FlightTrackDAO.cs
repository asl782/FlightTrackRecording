using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTrackRecording
{
    class FlightTrackDAO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Track { get; set; }
        public TimeSpan FlightLength { get; set; }
    }
}
