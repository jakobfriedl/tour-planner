using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer.Abstract;

namespace TourPlanner.BusinessLayer
{
    public static class ManagerFactory {
	    private static ITourManager _tourManager;
	    private static ILogManager _logManager;
	    private static IStatManager _statManager;

	    public static ITourManager GetTourManager(ILogger logger) {
		    return _tourManager ??= new TourManager(logger);
	    }

	    public static ILogManager GetLogManager(ILogger logger) {
		    return _logManager ??= new LogManager(logger); 
	    }

	    public static IStatManager GetStatManager(ILogger logger) {
		    return _statManager ??= new StatManager(logger); 
	    }
    }
}
