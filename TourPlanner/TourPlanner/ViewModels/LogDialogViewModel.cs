using System;
using System.Collections.Generic;
using System.Globalization;
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

        public LogDialogViewModel(LogListViewModel logListViewModel, Tour selectedTour, Log? logToUpdate, Action closeAction) {
	        SelectedTour = selectedTour;
	        LogDialogHeading = $"Create a new log for \"{SelectedTour.Name}\"";
	        
	        var isUpdate = false;
	        if (logToUpdate is not null) {
		        LogDialogLogId = logToUpdate.Id;
                // Convert DateTime object to correct string representation
		        LogDialogStartDateTime = $"{logToUpdate.StartTime.ToLongDateString()} {logToUpdate.StartTime.ToLongTimeString()}";
		        LogDialogEndDateTime = $"{logToUpdate.EndTime.ToLongDateString()} {logToUpdate.EndTime.ToLongTimeString()}"; 
		        LogDialogComment = logToUpdate.Comment;
		        LogDialogDifficulty = logToUpdate.Difficulty;
		        LogDialogRating = logToUpdate.Rating;
		        LogDialogHeading = $"Edit log for \"{SelectedTour.Name}\""; 
		        isUpdate = true;
            }
	        
	        SubmitCommand = new SubmitLogCommand(this, logListViewModel, isUpdate); 
	        CloseAction = closeAction;
        }

        public Log GetCreatedLog(Log log) {
	        return ManagerFactory.GetLogManager().CreateLog(log); 
        }

        public Log GetUpdatedLog(Log log) {
	        return ManagerFactory.GetLogManager().UpdateLog(log); 
        }
    }
}
