using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.REST
{
    public static class MapRequest
    {
	    public static async void GetTourInformation(Tour tour) {
		    var apiKey = "";
		    var distanceUnit = "k"; 
			var url =
			    $"http://www.mapquestapi.com/directions/v2/route?" +
			    $"key={apiKey}&from={tour.From}&to={tour.To}&unit={distanceUnit}";

		    var client = new HttpClient();

		    var res = JsonNode.Parse(await client.GetStringAsync(url));

		    tour.Distance = res["route"]["distance"].GetValue<double>();
		    tour.EstimatedTime = res["route"]["time"].GetValue<double>();
	    }
    }
}
