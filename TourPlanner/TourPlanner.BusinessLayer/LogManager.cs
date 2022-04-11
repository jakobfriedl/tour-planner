using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public class LogManager : ILogManager
    {
	    public Log CreateLog(Log log) {
		    throw new NotImplementedException();
	    }

	    public Log EditLog(Log log) {
		    throw new NotImplementedException();
	    }

	    public IEnumerable<Log> GetLogs(int tourId) {
		    throw new NotImplementedException();
	    }
    }
}
