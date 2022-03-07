using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
	public class Tour {
		public string TourName { get; set; }

		public Tour(string tourName) {
			TourName = tourName;
		}
	}
}
