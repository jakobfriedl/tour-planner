namespace TourPlanner.DataAccessLayer.DAO
{
    public interface IStatDAO {
	    int GetLogCount(int id);
	    double GetAvgRating(int id);
	    double GetAvgDifficulty(int id);
	    int GetAvgDuration(int id); 
	    double GetPopularity(int id);
	    double GetChildFriendliness(int id);
    }
}
