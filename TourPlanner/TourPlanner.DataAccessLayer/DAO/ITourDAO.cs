using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourDAO {
	    Tour AddNewTour(Tour tour);
	    bool DeleteTour(int id);
	    Tour UpdateTour(Tour tour); 
	    int SetImagePath(int id, string imagePath);
	    IEnumerable<Tour> GetTours();
    }
}
