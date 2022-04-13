using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Config;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourManager {
	    Tour GetTour(int tourId); 
	    Task<Tour> CreateTour(Tour tour);
	    Tour UpdateTour(Tour tour); 
	    IEnumerable<Tour> GetTours();
    }
}
