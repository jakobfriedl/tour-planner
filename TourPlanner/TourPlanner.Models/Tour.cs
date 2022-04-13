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
		public string TourName { get; set; }
		public string TourDescription { get; set; }
		public string Start { get; set; }
		public string Destination { get; set; }
		public TransportType TransportType { get; set; }
		public double? Distance { get; set; }
		public double? EstimatedTime { get; set; }
		public string? ImagePath { get; set; }

		public Tour(string tourName) {
			TourName = tourName; 
		}

		public Tour(string tourName, string tourDescription, string tourStart, string tourDestination,
			TransportType transportType) {
			TourName = tourName;
			TourDescription = tourDescription;
			Start = tourStart;
			Destination = tourDestination;
			TransportType = transportType; 
		}

		public Tour(int id, string tourName, string tourDescription, string start, string destination, TransportType transportType, double? distance, double? estimatedTime, string? imagePath) {
			Id = id;
			TourName = tourName;
			TourDescription = tourDescription;
			Start = start;
			Destination = destination;
			TransportType = transportType;
			Distance = distance;
			EstimatedTime = estimatedTime;
			ImagePath = imagePath; 
		}
	}
}
