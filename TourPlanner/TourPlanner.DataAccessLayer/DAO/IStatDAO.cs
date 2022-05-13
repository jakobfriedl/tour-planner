using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface IStatDAO {
	    int GetLogCount(int id);
	    double GetAvgRating(int id);
	    double GetAvgDifficulty(int id);
	    double GetPopularity(int id);
	    double GetChildFriendliness(int id);
    }
}
