using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class LogDialogViewModel : BaseViewModel {
        public Action CloseAction { get; }
        public ICommand SubmitCommand { get; }

        public Tour SelectedTour { get; }

        public int LogDialogLogId { get; set; } = 0;
        public string LogDialogStartDateTime { get; set; } = string.Empty;
        public string LogDialogEndDateTime { get; set; } = string.Empty;
        public string LogDialogComment { get; set; } = string.Empty;
        public int LogDialogDifficulty { get; set; } = 0;
        public int LogDialogRating { get; set; } = 0; 

        public string LogDialogHeading { get; set; }

        public LogDialogViewModel(LogListViewModel logListViewModel, Tour selectedTour, Action closeAction) {
	        SelectedTour = selectedTour; 
	        LogDialogHeading = $"Create a new log for \"{SelectedTour.Name}\"";

	        SubmitCommand = new SubmitLogCommand(this, logListViewModel, false); 
	        CloseAction = closeAction;
        }

        public Log GetCreatedLog(Log log) {
	        return ManagerFactory.GetLogManager().CreateLog(log); 
        }
    }
}
