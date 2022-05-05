using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class LogManager : ILogManager {
	    private readonly LogDAO _logDao;

	    public LogManager() {
		    _logDao = new LogDAO(new Database()); 
	    }

	    public LogManager(Database db) {
		    _logDao = new LogDAO(db); 
	    }

	    public Log CreateLog(Log log) {
			return _logDao.AddNewLog(log);
	    }

	    public Log UpdateLog(Log log) {
		    throw new NotImplementedException();
	    }

	    public bool DeleteLog(int id) {
		    return _logDao.DeleteLog(id); 
	    }

	    public IEnumerable<Log> GetLogs(int tourId) {
		    return _logDao.GetLogsByTourId(tourId); 
	    }
    }
}
