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
        public DateTime DateTime { get; set; }
        public int TotalTime { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }

		public string DisplayTime { get; set; }

        public Log(int tourId, DateTime dateTime, int totalTime, string comment, int difficulty, int rating) {
	        TourId = tourId;
	        DateTime = dateTime;
	        TotalTime = totalTime;
	        Comment = comment;
	        Difficulty = difficulty;
	        Rating = rating;
        }

        public Log(int id, int tourId, DateTime dateTime, int totalTime, string comment, int difficulty, int rating) {
	        Id = id;
	        TourId = tourId;
	        DateTime = dateTime;
	        TotalTime = totalTime;
	        Comment = comment;
	        Difficulty = difficulty;
	        Rating = rating;

	        DisplayTime = FormatTime(TotalTime).ToString("G").Split(",")[0]; 
        }

        private TimeSpan FormatTime(int time) {
	        var s = time % 60;
	        var m = (time / 60) % 60;
	        var h = (time / 3600);
	        return new TimeSpan(h, m, s);
        }
	}
}
