using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.DataAccessLayer.SQL;

namespace TourPlanner.BusinessLayer
{
    public class StatManager : IStatManager {
		private readonly ILogger _logger;
	    private readonly IStatDAO _statDao;

	    public StatManager(ILogger logger) {
		    _logger = logger; 
		    _statDao = new StatDAO(new Database(), logger);
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

	    /// <summary>
	    /// Calculate Popularity: Count * Avg-Rating 
	    /// </summary>
	    /// <param name="id">Tour Id</param>
	    /// <returns>Popularity to 2 decimal digits</returns>
	    public double GetPopularity(int id) {
			return GetLogCount(id) * GetAvgRating(id);
		}

	    /// <summary>
	    /// Calculate Child-Friendliness: 10 - Avg-Difficulty
	    /// </summary>
	    /// <param name="id">Tour Id</param>
	    /// <returns>0 if there are no logs, otherwise child-friendliness to 2 decimal digits</returns>
		public double GetChildFriendliness(int id) {
		    return GetLogCount(id) <= 0 ? 0 : 10 - GetAvgDifficulty(id);
		}
    }
}
