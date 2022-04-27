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
    public class AddTourDialogViewModel : BaseViewModel {
		public Action CloseAction { get; }
	    public ICommand SubmitCommand { get; }

	    public int AddTourId { get; set; } = 0; 
	    public string AddTourName { get; set; } = string.Empty;
		public string AddTourDescription { get; set; } = string.Empty;
		public string AddTourStart { get; set; } = string.Empty;
		public string AddTourDestination { get; set; } = string.Empty;
		public TransportType AddTourTransportType { get; set; }

	    public string DialogHeading { get; set; } = "Create a new tour";
	    
        public AddTourDialogViewModel(TourListViewModel viewModel, Action closeAction, Tour? tourToUpdate) {
	        var isUpdate = false; 
	        if (tourToUpdate is not null) {
		        AddTourId = tourToUpdate.Id; 
		        AddTourName = tourToUpdate.Name;
		        AddTourDescription = tourToUpdate.Description;
		        AddTourStart = tourToUpdate.Start;
		        AddTourDestination = tourToUpdate.Destination;
		        AddTourTransportType = tourToUpdate.TransportType;
		        DialogHeading = $"Edit tour \"{tourToUpdate.Name}\"";
		        isUpdate = true;
	        }

	        SubmitCommand = new SubmitTourCommand(viewModel, this, isUpdate);
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