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
		public TransportType? TransportType { get; set; }
		public double? Distance { get; set; }
		public double? EstimatedTime { get; set; }
		public string? ImagePath { get; set; }

		public Tour(string tourName) {
			TourName = tourName;
		}
	}
}
