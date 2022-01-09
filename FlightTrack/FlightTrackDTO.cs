using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTrackRecording
{
    class FlightTrackDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan FlightLength { get; set; }
    }
}
