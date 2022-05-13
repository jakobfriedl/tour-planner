using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer.Abstract
{
    public interface IStatManager {
		int GetLogCount(int id);
		double GetAvgRating(int id);
		double GetAvgDifficulty(int id);
		int GetAvgDuration(int id);
		double GetPopularity(int id);
		double GetChildFriendliness(int id);
	}
}
