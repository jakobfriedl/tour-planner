﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class LogManager : ILogManager {
	    private readonly ILogDAO _logDao;

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
