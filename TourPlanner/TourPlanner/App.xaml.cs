using System;
using System.Windows;
using Microsoft.Extensions.Logging;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
	    private void App_OnStartup(object sender, StartupEventArgs e) {

		    var loggerFactory = LoggerFactory.Create(builder => {
			    builder.AddConsole();
		    });
		    var logger = loggerFactory.CreateLogger("TourPlanner-Logger");
		    logger.LogInformation($"Application started at {DateTime.UtcNow}"); 

		    var logListViewModel = new LogListViewModel(logger);
		    var tourListViewModel = new TourListViewModel(logger, logListViewModel);
			var menuStripViewModel = new MenuStripViewModel(logger, tourListViewModel, logListViewModel);
			var tourDetailsViewModel = new TourDetailsViewModel(tourListViewModel);
		    var searchBarViewModel = new SearchBarViewModel(logger, tourListViewModel);

		    var mainViewModel = new MainWindowViewModel(searchBarViewModel, menuStripViewModel, tourListViewModel,
			    tourDetailsViewModel, logListViewModel);

		    var main = new MainWindow(mainViewModel);

		    main.Show();
	    }
    }
}
