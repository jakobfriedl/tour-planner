using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer.Abstract
{
    public interface ILogManager {
	    Log CreateLog(Log log);
	    Log UpdateLog(Log log);
	    bool DeleteLog(int id);
	    IEnumerable<Log> GetLogs(int tourId); 
    }
}
