using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.SQL
{
    internal class LogDAO : ILogDAO {
	    private readonly IDatabase _db;

	    public LogDAO(IDatabase database) {
		    _db = database; 
	    }

	    public Log GetLogByLogId(int id) {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Log> GetLogsByTourId(int id) {
		    throw new NotImplementedException();
	    }

	    public Log AddNewLog(int tourId, Log log) {
		    throw new NotImplementedException();
	    }

	    public Log UpdateLog(Log log) {
		    throw new NotImplementedException();
	    }

	    public bool DeleteLog(int id) {
		    throw new NotImplementedException();
	    }
    }
}
