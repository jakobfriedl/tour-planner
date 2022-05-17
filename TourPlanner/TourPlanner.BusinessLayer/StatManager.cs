using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.DataAccessLayer.SQL;

namespace TourPlanner.BusinessLayer
{
    public class StatManager : IStatManager {

	    private readonly IStatDAO _statDao;

	    public StatManager() {
		    _statDao = new StatDAO(new Database());
	    }

	    public StatManager(IStatDAO statDao) {
		    _statDao = statDao;
	    }

		public int GetLogCount(int id) {
			return _statDao.GetLogCount(id);
		}

	    public double GetAvgRating(int id) {
		    return _statDao.GetAvgRating(id); 
	    }

	    public double GetAvgDifficulty(int id) {
		    return _statDao.GetAvgDifficulty(id); 
	    }

	    public int GetAvgDuration(int id) {
		    return _statDao.GetAvgDuration(id); 
	    }

	    public double GetPopularity(int id) {
		    return _statDao.GetPopularity(id); 
	    }

	    public double GetChildFriendliness(int id) {
		    return _statDao.GetChildFriendliness(id); 
	    }
    }
}
