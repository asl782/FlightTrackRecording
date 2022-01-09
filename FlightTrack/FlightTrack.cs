using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlightTrackRecording
{
    class FlightTrack
    {        
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Position> Track { get; set; }
        public TimeSpan FlightLength { get; set; }

        public void SaveInDb()   
        {
            var flightTrackDAO = new FlightTrackDAO()
            {
                Id = this.Id,                            
                Date = this.Date,
                Track = JsonConvert.SerializeObject(this.Track),
                FlightLength = this.FlightLength
            };

            using (var dbContext = new FlightTrackDbContext())
            {
                dbContext.Update(flightTrackDAO);
                dbContext.SaveChanges();
                this.Id = flightTrackDAO.Id;
            }           
        }

        public void SaveInFile(string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(this));
            }
        }

        public static bool DeleteById(int id)
        {
            bool result = false;
            FlightTrackDAO flightTrackDAO;

            using (var dbContext = new FlightTrackDbContext())
            {
                flightTrackDAO = dbContext.FlightTracks
                    .FirstOrDefault(f => f.Id == id);

                if (flightTrackDAO != null)
                {
                    dbContext.FlightTracks.Remove(flightTrackDAO);
                    dbContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public bool Delete()
        {
            var id = this.Id;
            var result = FlightTrack.DeleteById(id);
            return result;
        }

        public static FlightTrack GetById(int id)
        {
            var flightTrackDAO = new FlightTrackDAO();

            using (var dbContext = new FlightTrackDbContext())
            {
                flightTrackDAO = dbContext.FlightTracks
                    .FirstOrDefault(f => f.Id == id);
            }

            if (flightTrackDAO != null)
            {
                var flightTrack = new FlightTrack()
                {
                Id = flightTrackDAO.Id,
                Date = flightTrackDAO.Date,
                Track = JsonConvert.DeserializeObject<List<Position>>(flightTrackDAO.Track),
                FlightLength = flightTrackDAO.FlightLength,
                };

                return flightTrack;               
            }
            else
            {
                return null;
            }
        }

        public static List<FlightTrackDTO> OrderByFlightLength()
        {
            var flightTracksDAO = new List<FlightTrackDAO>();

            using (var dbContext = new FlightTrackDbContext())
            {
                flightTracksDAO = dbContext.FlightTracks
                    .AsEnumerable()
                    .OrderByDescending(f => f.FlightLength.TotalSeconds)
                    .ToList();
            }

            var flightTracksDTO = new List<FlightTrackDTO>();

            for(int i = 0; i < flightTracksDAO.Count; i++)
            {
                var flightTrackDTO = new FlightTrackDTO()
                {
                    Id = flightTracksDAO[i].Id,
                    Date = flightTracksDAO[i].Date,
                    FlightLength = flightTracksDAO[i].FlightLength
                };

                flightTracksDTO.Add(flightTrackDTO);
            }

            return flightTracksDTO;
        }
    }
}
