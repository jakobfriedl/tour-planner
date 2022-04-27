using System;
using System.Reflection.Metadata;
using System.Windows;
using Npgsql;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.Exceptions;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands {

	/// <summary>
	///     Button which validates input of AddTourDialog and sends it to the business layer
	/// </summary>
	internal class SubmitTourCommand : BaseCommand {
		public AddTourDialogViewModel AddTourDialogViewModel { get; }
		public TourListViewModel TourListViewModel { get; }
		public bool IsUpdate { get; }

		public SubmitTourCommand(TourListViewModel viewModel, AddTourDialogViewModel addTourDialogViewModel, bool isUpdate) {
			TourListViewModel = viewModel;
			AddTourDialogViewModel = addTourDialogViewModel;
			IsUpdate = isUpdate; 
		}

		/// <summary>
		/// Checks if all input fields are filled out by the user
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public override bool CanExecute(object? parameter) {
			return !string.IsNullOrEmpty(AddTourDialogViewModel.AddTourName) &&
			       !string.IsNullOrEmpty(AddTourDialogViewModel.AddTourDescription) &&
			       !string.IsNullOrEmpty(AddTourDialogViewModel.AddTourStart) &&
			       !string.IsNullOrEmpty(AddTourDialogViewModel.AddTourDestination) &&
			       base.CanExecute(parameter);
		}

		/// <summary>
		/// Executes Command, send TourData to backend (API Request and create Tour in DB)
		/// </summary>
		/// <param name="parameter"></param>
		public override async void Execute(object? parameter) {
			var tour = new Tour(AddTourDialogViewModel.AddTourId,
				AddTourDialogViewModel.AddTourName,
				AddTourDialogViewModel.AddTourDescription,
				AddTourDialogViewModel.AddTourStart,
				AddTourDialogViewModel.AddTourDestination,
				AddTourDialogViewModel.AddTourTransportType);

			try {
				if (!IsUpdate) {
					tour = await AddTourDialogViewModel.GetCreatedTour(tour);
					TourListViewModel.AddTour(tour); // Add new Tout to TourListViewModel to update List
				} else {
					tour = await AddTourDialogViewModel.GetUpdatedTour(tour); 
					TourListViewModel.ReplaceTour(tour);
				}
			} catch (InvalidLocationException) {
				// Invalid Location, show Error-MessageBox
				MessageBox.Show("Please make sure to enter valid locations.",
					"Invalid locations",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			AddTourDialogViewModel.CloseAction(); // Close Dialog Window
		}
	}
}