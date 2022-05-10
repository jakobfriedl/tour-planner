using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TourPlanner.DataAccessLayer.Configuration
{
    public static class ConfigManager
    {
	    public static TourPlannerConfig GetConfig() {
		    var configPath = Path.Combine(Directory.GetCurrentDirectory(), $"Config\\settings.json"); 
			    
		    IConfiguration config = new ConfigurationBuilder()
			    .AddJsonFile(configPath, false, true)
			    .Build();

		    return new TourPlannerConfig {
			    ImageLocation = config["image-location"],
			    DatabaseHost = config["db:host"],
			    DatabasePort = config["db:port"],
			    DatabaseUsername = config["db:username"],
			    DatabasePassword = config["db:password"],
			    DatabaseName = config["db:database"],
				ApiKey = config["api-key"]
		    }; 
	    }
    }
}
