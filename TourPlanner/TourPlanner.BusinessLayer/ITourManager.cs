using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourManager {
	    Task<Tour> CreateTour(Tour tour);
	    Tour UpdateTour(Tour tour);
	    bool DeleteTour(int id); 
	    IEnumerable<Tour> GetTours();
    }
}
