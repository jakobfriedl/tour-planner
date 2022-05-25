using System;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class TourDialogViewModel : BaseViewModel {
		public Action CloseAction { get; }
	    public ICommand SubmitCommand { get; }

		public TourListViewModel TourListViewModel { get; }

	    public int TourDialogId { get; set; } = 0; 
	    public string TourDialogName { get; set; } = string.Empty;
		public string TourDialogDescription { get; set; } = string.Empty;
		public string TourDialogStart { get; set; } = string.Empty;
		public string TourDialogDestination { get; set; } = string.Empty;
		public int TourDialogTransportType { get; set; } = 0; 

	    public string TourDialogHeading { get; set; } = "Create a new tour";
	    
        public TourDialogViewModel(TourListViewModel tourListViewModel, Action closeAction, Tour? tourToUpdate) {
	        TourListViewModel = tourListViewModel; 

	        var isUpdate = false; 
	        if (tourToUpdate is not null) {
		        TourDialogId = tourToUpdate.Id; 
		        TourDialogName = tourToUpdate.Name;
		        TourDialogDescription = tourToUpdate.Description;
		        TourDialogStart = tourToUpdate.Start;
		        TourDialogDestination = tourToUpdate.Destination;
		        TourDialogHeading = $"Edit tour \"{tourToUpdate.Name}\"";
		        isUpdate = true;
	        }

	        SubmitCommand = new SubmitTourCommand(this, tourListViewModel, isUpdate);
			CloseAction = closeAction; 
        }
    }
}