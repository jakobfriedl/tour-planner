using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
	public class Tour {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Start { get; set; }
		public string Destination { get; set; }
		public TransportType TransportType { get; set; }
		public double Distance { get; set; } = 0;
		public int EstimatedTime { get; set; } = 0; 
		public string ImagePath { get; set; }
		public double? Popularity { get; set; } = 0;
		public double? ChildFriendliness { get; set; } = 0;

		public string DisplayDistance { get; set; }
		public string DisplayTime { get; set; }

		public byte[] RouteImageSource { get; set; }

		public Tour(string name) {
			Name = name;
		}

		public Tour(int id, string name, string description, string tourStart, string tourDestination,
			TransportType transportType) {
			Id = id;
			Name = name;
			Description = description;
			Start = tourStart;
			Destination = tourDestination;
			TransportType = transportType;
		}

		public Tour(int id, string name, string description, string start, string destination, TransportType transportType, double distance, int estimatedTime, string imagePath, double popularity, double childFriendliness) {
			Id = id;
			Name = name;
			Description = description;
			Start = start;
			Destination = destination;
			TransportType = transportType;
			Distance = distance;
			EstimatedTime = estimatedTime;
			ImagePath = imagePath;
			Popularity = popularity;
			ChildFriendliness = childFriendliness;

			DisplayDistance = $"{Math.Round(distance, 2)} km";
			DisplayTime = FormatTime(EstimatedTime).ToString("G").Split(',')[0]; 
		}

		private TimeSpan FormatTime(int time) {
			var s = time % 60;
			var m = (time / 60) % 60;
			var h = (time / 3600);
			return new TimeSpan(h, m, s);
		}
	}
}
