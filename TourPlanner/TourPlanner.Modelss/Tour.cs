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
		public string From { get; set; }
		public string To { get; set; }
		public TransportType TransportType { get; set; }
		public double? Distance { get; set; }
		public double? EstimatedTime { get; set; }
		public string? ImagePath { get; set; }

		public Tour(string tourName) {
			TourName = tourName; 
		}

		public Tour(string tourName, string tourDescription, string tourFrom, string tourTo,
			TransportType transportType) {
			TourName = tourName;
			TourDescription = tourDescription;
			From = tourFrom;
			To = tourTo;
			TransportType = transportType; 
		}

		public Tour(int id, string tourName, string tourDescription, string from, string to, TransportType transportType, double? distance, double? estimatedTime, string? imagePath) {
			Id = id;
			TourName = tourName;
			TourDescription = tourDescription;
			From = from;
			To = to;
			TransportType = transportType;
			Distance = distance;
			EstimatedTime = estimatedTime;
			ImagePath = imagePath; 
		}
	}
}
