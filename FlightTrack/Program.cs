using System;
using System.Collections.Generic;

namespace FlightTrackRecording
{
    class Program
    {
        static void Main(string[] args)
        {
            var track1 = new FlightTrack()
            {
                Date = new DateTime(2018, 2, 4, 12, 3, 4),
                Track = new List<Position>(),
                FlightLength = new TimeSpan(1, 2, 0)
            };

            track1.Track.Add(new Position() { Latitude = 45.68D, Longitude = 25.68D, Time = new DateTime(2018, 2, 4, 12, 45, 0) });
            track1.Track.Add(new Position() { Latitude = 45.90D, Longitude = 24.90D, Time = new DateTime(2018, 2, 4, 13, 25, 9) });
            track1.Track.Add(new Position() { Latitude = 44.90D, Longitude = 23.90D, Time = new DateTime(2018, 2, 4, 13, 45, 17) });

            track1.SaveInDb();
            Console.WriteLine($"Track1 Id = {track1.Id}");

            var track2 = new FlightTrack()
            {
                Date = new DateTime(2019, 2, 4, 12, 3, 4),
                Track = new List<Position>(),
                FlightLength = new TimeSpan(0, 45, 43)
            };

            track2.Track.Add(new Position() { Latitude = 30.68D, Longitude = 25.68D, Time = new DateTime(2019, 2, 4, 12, 45, 0) });
            track2.Track.Add(new Position() { Latitude = 31.90D, Longitude = 24.90D, Time = new DateTime(2019, 2, 4, 13, 25, 9) });
            track2.Track.Add(new Position() { Latitude = 32.90D, Longitude = 23.90D, Time = new DateTime(2019, 2, 4, 13, 45, 17) });

            track2.SaveInDb();

            var track3 = new FlightTrack()
            {
                Date = new DateTime(2019, 2, 15, 12, 3, 4),
                Track = new List<Position>(),
                FlightLength = new TimeSpan(1, 1, 50)
            };

            track3.Track.Add(new Position() { Latitude = 30.68D, Longitude = 25.68D, Time = new DateTime(2019, 2, 15, 12, 45, 0) });
            track3.Track.Add(new Position() { Latitude = 31.90D, Longitude = 24.90D, Time = new DateTime(2019, 2, 15, 13, 25, 9) });
            track3.Track.Add(new Position() { Latitude = 32.90D, Longitude = 23.90D, Time = new DateTime(2019, 2, 15, 13, 45, 17) });

            track3.SaveInDb();

            var list = FlightTrack.OrderByFlightLength();
            Display(list);

            track2.SaveInFile(@"D:\MyFlights.txt");
            track3.SaveInFile(@"D:\MyFlights.txt");

            track3.FlightLength = new TimeSpan(2, 1, 5);
            track3.SaveInDb();

            var deleted = track2.Delete();
            Console.WriteLine(deleted);

            list = FlightTrack.OrderByFlightLength();
            Display(list);

            var deleted2 = FlightTrack.DeleteById(2);
            Console.WriteLine(deleted2);

            var result = FlightTrack.GetById(2);
            Console.WriteLine(result);

            track3 = FlightTrack.GetById(3);
            Console.WriteLine($"Track3 id = {track3.Id}, flight length = {track3.FlightLength}, initial latitude = {track3.Track[0].Latitude}");
        }

        static void Display(List<FlightTrackDTO> list)
        {
            foreach (var f in list)
            {
                Console.WriteLine($"Id: {f.Id}, date: {f.Date}, flight length: {f.FlightLength}");
            }
        }
    }
}