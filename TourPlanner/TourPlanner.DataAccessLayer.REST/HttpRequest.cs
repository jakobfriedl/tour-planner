using System;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.REST
{
    public class HttpRequest : IHttpRequest
    {
		private readonly string? _key = ConfigManager.GetConfig().ApiKey;
		private readonly HttpClient _client; 

		public HttpRequest(HttpClient client) {
			_client = client; 
		}

		public async Task<Tour> GetTourInformation(Tour tour) {
		    var url = "http://www.mapquestapi.com/directions/v2/route?" +
		              $"key={_key}&from={tour.Start}&to={tour.Destination}&unit=k";

		    var json = JsonNode.Parse(await _client.GetStringAsync(url));
		    tour.Distance = json["route"]["distance"].GetValue<double>();
		    tour.EstimatedTime = json["route"]["time"].GetValue<int>();

		    return tour;
		}

	    public async Task<byte[]> GetTourImageBytes(Tour tour) {
		    var url = "https://open.mapquestapi.com/staticmap/v5/map?" +
		              $"key={_key}&start={tour.Start}&end={tour.Destination}";
		    return await _client.GetByteArrayAsync(url) ;
	    }
    }
}
