using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer
{
    public static class ManagerFactory {
	    private static ITourManager _tourManager;
	    private static ILogManager _logManager;

	    public static ITourManager GetTourManager() {
		    return _tourManager ??= new TourManager();
	    }

	    public static ILogManager GetLogManager() {
		    return _logManager ??= new LogManager(); 
	    }
    }
}
