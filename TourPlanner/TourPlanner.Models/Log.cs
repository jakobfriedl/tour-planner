using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Log {
	    public int Id { get; set; }
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
		public string Start { get; set; }
		public string Destination { get; set; }
        public int TotalTime { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }

		public string DisplayTime { get; set; }

        public Log(int tourId, DateTime startTime, DateTime endTime, int totalTime, string start, string destination, string comment, int difficulty, int rating) {
	        TourId = tourId;
	        StartTime = startTime; 
	        EndTime = endTime;
	        TotalTime = totalTime;
			Start = start;
			Destination = destination;
	        Comment = comment;
	        Difficulty = difficulty;
	        Rating = rating;
        }

        public Log(int id, int tourId, DateTime startTime, DateTime endTime, int totalTime, string start, string destination, string comment, int difficulty, int rating) {
	        Id = id;
	        TourId = tourId;
	        StartTime = startTime; 
	        EndTime = endTime;
			TotalTime = totalTime;
	        Start = start;
	        Destination = destination; 
	        Comment = comment;
	        Difficulty = difficulty;
	        Rating = rating;

	        DisplayTime = TimeSpan.FromSeconds(TotalTime).ToString("G").Split(",")[0]; 
        }
    }
}
