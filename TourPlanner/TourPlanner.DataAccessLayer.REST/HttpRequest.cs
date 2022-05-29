using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.REST
{
    public class HttpRequest : IHttpRequest {
	    private readonly ILogger _logger; 
		private readonly string? _key = ConfigManager.GetConfig().ApiKey;
		private readonly HttpClient _client; 

		public HttpRequest(ILogger logger, HttpClient client) {
			_logger = logger;
			_client = client; 
		}

		/// <summary>
		/// Get Tour Information from MapQuest API (Distance and Time)
		/// </summary>
		/// <param name="tour">Tour to get information from</param>
		/// <returns>Tour with all distance and time information</returns>
		public virtual async Task<Tour> GetTourInformation(Tour tour) {
			var url = "http://www.mapquestapi.com/directions/v2/route?" + 
			          $"key={_key}&from={tour.Start}&to={tour.Destination}&unit=k";
			
			var json = JsonNode.Parse(await _client.GetStringAsync(url));
			tour.Distance = json["route"]["distance"].GetValue<double>();
			tour.EstimatedTime = json["route"]["time"].GetValue<int>();
			return tour;
		}

		/// <summary>
		/// Get Route image from MapQuest Static Map API 
		/// </summary>
		/// <param name="tour">Tour to get image from</param>
		/// <returns>Image as byte array</returns>
	    public virtual async Task<byte[]> GetTourImageBytes(Tour tour) {
		    var url = "https://open.mapquestapi.com/staticmap/v5/map?" +
		              $"key={_key}&start={tour.Start}&end={tour.Destination}";
			return await _client.GetByteArrayAsync(url);
		}
    }
}
