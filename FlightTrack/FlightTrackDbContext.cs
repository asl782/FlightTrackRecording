using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTrackRecording
{
    class FlightTrackDbContext : DbContext
    {
        public DbSet<FlightTrackDAO> FlightTracks { get; set; }

        public string ConnectionString { get; private set; }

        public FlightTrackDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            ConnectionString = $"{path}{System.IO.Path.DirectorySeparatorChar}flightTrack.db";
        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ConnectionString}");
        }
    }
}
