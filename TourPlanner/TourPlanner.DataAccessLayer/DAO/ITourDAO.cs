using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ITourDAO {
	    Tour GetTourByTourId(int id); 
	    Tour AddNewTour(Tour tour);
	    bool DeleteTour(int id); 
	    int SetImagePath(int id, string imagePath);
	    IEnumerable<Tour> GetTours();
    }
}
