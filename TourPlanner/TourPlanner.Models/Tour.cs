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
		public double EstimatedTime { get; set; } = 0; 
		public string ImagePath { get; set; }
		public int? Popularity { get; set; } = 0;
		public int? ChildFriendliness { get; set; } = 0;

		public Tour(string name) {
			Name = name;
		}

		public Tour(string name, string description, string tourStart, string tourDestination,
			TransportType transportType) {
			Name = name;
			Description = description;
			Start = tourStart;
			Destination = tourDestination;
			TransportType = transportType;
		}

		public Tour(int id, string name, string description, string start, string destination, TransportType transportType, double distance, double estimatedTime, string imagePath, int popularity, int childFriendliness) {
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
		}
	}
}
