using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourDAO {
	    Tour AddNewTour(Tour tour);
	    bool DeleteTour(int id);
	    Tour UpdateTour(Tour tour); 
	    void SetImagePath(int id, string imagePath);
	    IEnumerable<Tour> GetTours();
	    IEnumerable<Tour> SearchTours(string searchTerm);
    }
}
