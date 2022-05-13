using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.Abstract;

namespace TourPlanner.BusinessLayer
{
    public static class ManagerFactory {
	    private static ITourManager _tourManager;
	    private static ILogManager _logManager;
	    private static IStatManager _statManager;

	    public static ITourManager GetTourManager() {
		    return _tourManager ??= new TourManager();
	    }

	    public static ILogManager GetLogManager() {
		    return _logManager ??= new LogManager(); 
	    }

	    public static IStatManager GetStatManager() {
		    return _statManager ??= new StatManager(); 
	    }
    }
}
