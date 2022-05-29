using System.Collections.Generic;
using Newtonsoft.Json;

namespace TourPlanner.Models.Json {
	public class TourObject {

		[JsonProperty("Tour")] public Tour Tour { get; set; }
		[JsonProperty("Image")] public string ImageInBase64 { get; set; }
		[JsonProperty("Logs")] public List<Log> Logs { get; set; }

		public TourObject(Tour tour, string image, List<Log> logs) {
			Tour = tour;
			ImageInBase64 = image;
			Logs = logs;
		}

	}
}