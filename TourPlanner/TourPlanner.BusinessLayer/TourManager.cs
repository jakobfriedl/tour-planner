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
using TourPlanner.DataAccessLayer.SQL;
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

		    var tourDao = new TourDAO(new Database());
		    tour = tourDao.AddNewTour(tour); 

			// Save image from REST Request to png-File
		    var imageBytes = await http.GetTourImageBytes(tour);
		    tour.ImagePath = $"{ConfigManager.GetConfig().ImageLocation}\\{tour.Id}.png";
		    await File.WriteAllBytesAsync(tour.ImagePath, imageBytes);

		    tourDao.SetImagePath(tour.Id, tour.ImagePath); 

		    return tour;
	    }

	    public Tour UpdateTour(Tour tour) {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Tour> GetTours() {
			var tourDao = new TourDAO(new Database());
			return tourDao.GetTours();
	    }
    }
}
