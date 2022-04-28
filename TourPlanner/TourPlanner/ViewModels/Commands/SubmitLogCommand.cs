using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class SubmitLogCommand : BaseCommand
    {
        public LogDialogViewModel LogDialogViewModel { get; }
        public LogListViewModel LogListViewModel { get; }
        public bool IsUpdate { get; }

        public SubmitLogCommand(LogDialogViewModel logDialogViewModel, LogListViewModel logListViewModel, bool isUpdate) {
	        LogDialogViewModel = logDialogViewModel;
	        LogListViewModel = logListViewModel;
            IsUpdate = isUpdate;
        }

        public override bool CanExecute(object? parameter) {
	        return !string.IsNullOrEmpty(LogDialogViewModel.LogDialogDateTime.ToString()) &&
	               !string.IsNullOrEmpty(LogDialogViewModel.LogDialogTotalTime.ToString()) &&
	               !string.IsNullOrEmpty(LogDialogViewModel.LogDialogComment) &&
	               LogDialogViewModel.LogDialogDifficulty >= 0 && LogDialogViewModel.LogDialogDifficulty <= 10 &&
	               LogDialogViewModel.LogDialogRating >= 0 && LogDialogViewModel.LogDialogRating <= 10 &&
	               base.CanExecute(parameter);
        }

        public override void Execute(object? parameter) {
	        var log = new Log(LogDialogViewModel.TourListViewModel.SelectedTour.Id,
		        LogDialogViewModel.LogDialogDateTime,
		        (int)LogDialogViewModel.LogDialogTotalTime.TotalSeconds,
		        LogDialogViewModel.LogDialogComment,
		        LogDialogViewModel.LogDialogDifficulty,
		        LogDialogViewModel.LogDialogRating);

	        LogDialogViewModel.CloseAction(); 
        }
    }
}
