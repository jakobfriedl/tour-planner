using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class LogDialogViewModel : BaseViewModel {
        public Action CloseAction { get; }
        public ICommand SubmitCommand { get; }

        public TourListViewModel TourListViewModel { get; }

        public int LogDialogLogId { get; set; } = 0;
        public DateTime LogDialogDateTime { get; set; }
        public TimeSpan LogDialogTotalTime { get; set; }
        public string LogDialogComment { get; set; } = string.Empty; 
        public int LogDialogDifficulty { get; set; } 
        public int LogDialogRating { get; set; }

        public string LogDialogHeading { get; set; }

        public LogDialogViewModel(LogListViewModel logListViewModel, TourListViewModel tourListViewModel, Action closeAction) {
	        TourListViewModel = tourListViewModel; 
	        LogDialogHeading = $"Create a new log for \"{TourListViewModel.SelectedTour.Name}\"";

	        SubmitCommand = new SubmitLogCommand(this, logListViewModel, false); 
	        CloseAction = closeAction;
        }
    }
}
