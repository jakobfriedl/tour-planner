using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer.Abstract
{
    public interface ITourManager {
	    Task<Tour> CreateTour(Tour tour);
	    Task<Tour> UpdateTour(Tour tour);
	    bool DeleteTour(int id); 
	    IEnumerable<Tour> GetTours();
	    IEnumerable<Tour> SearchTours(string searchTerm);
	    Task<Tour> GetInformation(Tour tour);
	    Task<Tour> SaveInformation(Tour tour);
    }
}
