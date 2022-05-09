using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO
{
    public interface ILogDAO {
	    IEnumerable<Log> GetLogsByTourId(int tourId);
	    Log AddNewLog(Log log);
	    Log UpdateLog(Log log);
	    bool DeleteLog(int id);
    }
}
