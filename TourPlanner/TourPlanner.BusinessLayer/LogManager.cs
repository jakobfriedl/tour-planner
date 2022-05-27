using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class LogManager : ILogManager {
		private readonly ILogger _logger;
	    private readonly ILogDAO _logDao;

	    public LogManager(ILogger logger) {
		    _logger = logger; 
		    _logDao = new LogDAO(new Database(), logger); 
	    }

	    public LogManager(ILogDAO logDao) {
		    _logDao = logDao; 
	    }

	    public Log CreateLog(Log log) {
			return _logDao.AddNewLog(log);
	    }

	    public Log UpdateLog(Log log) {
		    return _logDao.UpdateLog(log); 
	    }

	    public bool DeleteLog(int id) {
		    return _logDao.DeleteLog(id); 
	    }

	    public IEnumerable<Log> GetLogs(int tourId) {
		    return _logDao.GetLogsByTourId(tourId); 
	    }
    }
}
