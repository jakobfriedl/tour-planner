using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels;
using TourPlanner.Views;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
	    private void App_OnStartup(object sender, StartupEventArgs e) {
		    var logListViewModel = new LogListViewModel();
		    var tourListViewModel = new TourListViewModel(logListViewModel);
			var menuStripViewModel = new MenuStripViewModel(tourListViewModel, logListViewModel);
			var tourDetailsViewModel = new TourDetailsViewModel(tourListViewModel);
		    var searchBarViewModel = new SearchBarViewModel(tourListViewModel);

		    var mainViewModel = new MainWindowViewModel(searchBarViewModel, menuStripViewModel, tourListViewModel,
			    tourDetailsViewModel, logListViewModel);

		    var main = new MainWindow(mainViewModel);

		    main.Show();
	    }
    }
}
