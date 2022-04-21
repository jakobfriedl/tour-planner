using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
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
		    tour = await http.GetTourInformation(tour);
		    // var image = await http.GetTourImage(tour);
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
