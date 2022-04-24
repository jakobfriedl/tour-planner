using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenSettingsCommand : BaseCommand {
	    private readonly string _settingsLocation;
	    private readonly string _application; 

	    public OpenSettingsCommand() {
		    _settingsLocation = $"{Directory.GetCurrentDirectory()}\\Config";
		    _application = "explorer.exe"; 
	    }

	    public OpenSettingsCommand(string settingsLocation, string application) {
		    _settingsLocation = settingsLocation;
		    _application = application; 
	    }

        public override void Execute(object? parameter) {
	        if (Directory.Exists(_settingsLocation)) {
		        var startInfo = new ProcessStartInfo(_application, _settingsLocation);
		        Process.Start(startInfo); 
	        } else {
		        MessageBox.Show($"Directory {_settingsLocation} does not exist.", "Directory does not exist", MessageBoxButton.OK, MessageBoxImage.Error); 
	        }
	    }
    }
}
