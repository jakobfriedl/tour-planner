using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Exceptions;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class TourDialogViewModel : BaseViewModel {
		public Action CloseAction { get; }
	    public ICommand SubmitCommand { get; }

	    public int TourDialogId { get; set; } = 0; 
	    public string TourDialogName { get; set; } = string.Empty;
		public string TourDialogDescription { get; set; } = string.Empty;
		public string TourDialogStart { get; set; } = string.Empty;
		public string TourDialogDestination { get; set; } = string.Empty;
		public TransportType TourDialogTransportType { get; set; }

	    public string TourDialogHeading { get; set; } = "Create a new tour";
	    
        public TourDialogViewModel(TourListViewModel tourListViewModel, Action closeAction, Tour? tourToUpdate) {
	        var isUpdate = false; 
	        if (tourToUpdate is not null) {
		        TourDialogId = tourToUpdate.Id; 
		        TourDialogName = tourToUpdate.Name;
		        TourDialogDescription = tourToUpdate.Description;
		        TourDialogStart = tourToUpdate.Start;
		        TourDialogDestination = tourToUpdate.Destination;
		        TourDialogTransportType = tourToUpdate.TransportType;
		        TourDialogHeading = $"Edit tour \"{tourToUpdate.Name}\"";
		        isUpdate = true;
	        }

	        SubmitCommand = new SubmitTourCommand(this, tourListViewModel, isUpdate);
			CloseAction = closeAction; 
        }

		public async Task<Tour> GetCreatedTour(Tour tour){
			try {
				return await ManagerFactory.GetTourManager().CreateTour(tour);
			} catch (InvalidLocationException) { throw; }
		}

		public async Task<Tour> GetUpdatedTour(Tour tour) {
			try {
				return await ManagerFactory.GetTourManager().UpdateTour(tour);
			} catch (InvalidLocationException) { throw; }
		}
	}
}