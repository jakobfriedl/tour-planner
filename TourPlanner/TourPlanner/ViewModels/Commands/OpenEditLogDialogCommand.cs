using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.Views;

namespace TourPlanner.ViewModels.Commands
{
    public class OpenEditLogDialogCommand : BaseCommand
    {
	    public LogListViewModel LogListViewModel { get; set; }
		public Tour SelectedTour { get; set; }
	    public Log LogToUpdate { get; set; }

	    public OpenEditLogDialogCommand(LogListViewModel logListViewModel, Tour selectedTour) {
		    LogListViewModel = logListViewModel;
			SelectedTour = selectedTour;
		    LogToUpdate = LogListViewModel.SelectedLog;
	    }

	    public override bool CanExecute(object? parameter) {
		    return !LogListViewModel.IsEmpty() &&
		           base.CanExecute(parameter);
	    }

	    public override void Execute(object? parameter) {
		    var dialog = new LogDialog(LogListViewModel, SelectedTour, LogToUpdate);
		    dialog.ShowDialog();
	    }
	}
}
