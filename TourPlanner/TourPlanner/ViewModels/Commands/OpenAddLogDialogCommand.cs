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
    public class OpenAddLogDialogCommand : BaseCommand
    {
	    public LogListViewModel LogListViewModel { get; }
		public TourListViewModel TourListViewModel { get; }

	    public OpenAddLogDialogCommand(LogListViewModel vm, TourListViewModel tourListViewModel) {
		    LogListViewModel = vm;
		    TourListViewModel = tourListViewModel; 
	    }

	    public override void Execute(object? parameter) {
		    var dialog = new LogDialog(LogListViewModel, TourListViewModel);
		    dialog.ShowDialog();
	    }
	}
}
