using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TourPlanner.DataAccessLayer.Config
{
    public static class ConfigManager
    {
	    public static TourPlannerConfig GetConfig() {
		    var configPath =
			    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent 
			    + "\\config\\settings.json";

		    IConfiguration config = new ConfigurationBuilder()
			    .AddJsonFile(configPath, false, true)
			    .Build();

		    return new TourPlannerConfig() {
			    ImageLocation = config["image-location"],
			    DatabaseHost = config["db:host"],
			    DatabaseUsername = config["db:username"],
			    DatabasePassword = config["db:password"],
			    DatabaseName = config["db:database"]
		    }; 
	    }
    }
}
