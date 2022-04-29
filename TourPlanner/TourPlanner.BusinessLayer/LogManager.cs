using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class LogManager : ILogManager
    {
	    public Log CreateLog(Log log) {
		    var logDAO = new LogDAO(new Database());
			return logDAO.AddNewLog(log);
	    }

	    public Log UpdateLog(Log log) {
		    throw new NotImplementedException();
	    }

	    public bool DeleteLog(int id) {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Log> GetLogs(int tourId) {
		    var logDAO = new LogDAO(new Database());
		    return logDAO.GetLogsByTourId(tourId); 
	    }
    }
}
