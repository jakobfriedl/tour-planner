using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.DataAccessLayer.REST;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourManager : ITourManager
    {
	    public Tour GetTour(int tourId) {
		    throw new NotImplementedException();
	    }

	    public async Task<Tour> CreateTour(Tour tour) {
		    var http = new HttpRequest(new HttpClient());

		    try {
			    tour = await http.GetTourInformation(tour);
		    } catch (NullReferenceException) { throw; }

		    var imageBytes = await http.GetTourImageBytes(tour);
		    tour.ImagePath =
			    $"{Directory.GetCurrentDirectory()}\\{ConfigManager.GetConfig().ImageLocation}\\{tour.TourName}.png";
		    await File.WriteAllBytesAsync(tour.ImagePath, imageBytes);

		    return tour;
	    }

	    public Tour UpdateTour(Tour tour) {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Tour> GetTours() {
		    throw new NotImplementedException();
	    }
    }
}
