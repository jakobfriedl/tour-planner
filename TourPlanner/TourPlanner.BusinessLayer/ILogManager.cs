using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ILogManager {
	    Log CreateLog(Log log);
	    Log EditLog(Log log);
	    IEnumerable<Log> GetLogs(int tourId); 
    }
}
