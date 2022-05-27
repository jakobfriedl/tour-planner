﻿using Newtonsoft.Json;
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
        public int TotalTime { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }

		public string DisplayTime { get; set; }

        [JsonConstructor]
		public Log(int id, int tourId, DateTime startTime, DateTime endTime, int totalTime, string comment, int difficulty, int rating, string displayTime)
		{
			Id = id;
			TourId = tourId;
			StartTime = startTime;
			EndTime = endTime;
			TotalTime = totalTime;
			Comment = comment;
			Difficulty = difficulty;
			Rating = rating;
			DisplayTime = displayTime;
		}

		public Log(int tourId, DateTime startTime, DateTime endTime, int totalTime, string comment, int difficulty, int rating) {
	        TourId = tourId;
	        StartTime = startTime; 
	        EndTime = endTime;
	        TotalTime = totalTime;
	        Comment = comment;
	        Difficulty = difficulty;
	        Rating = rating;
        }

        public Log(int id, int tourId, DateTime startTime, DateTime endTime, int totalTime, string comment, int difficulty, int rating) {
	        Id = id;
	        TourId = tourId;
	        StartTime = startTime; 
	        EndTime = endTime;
	        TotalTime = totalTime;
	        Comment = comment;
	        Difficulty = difficulty;
	        Rating = rating;

	        DisplayTime = TimeSpan.FromSeconds(TotalTime).ToString("G").Split(",")[0]; 
        }
    }
}
