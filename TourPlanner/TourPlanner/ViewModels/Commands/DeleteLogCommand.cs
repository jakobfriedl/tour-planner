using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class DeleteLogCommand : BaseCommand {
        public LogListViewModel LogListViewModel { get; }

        public DeleteLogCommand(LogListViewModel logListViewModel) {
	        LogListViewModel = logListViewModel;
        }

        public override bool CanExecute(object? parameter) {
	        return !LogListViewModel.IsEmpty() &&
	               base.CanExecute(parameter);
        }

        public override void Execute(object? parameter) {
	        if (!LogListViewModel.DeleteLog()) {
		        MessageBox.Show("Could not delete selected log", "Log deletion error", MessageBoxButton.OK,
			        MessageBoxImage.Error);
		        return;
	        }
	        LogListViewModel.RemoveSelectedLog();
        }
	}
}
