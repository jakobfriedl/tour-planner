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
	///     Button which validates input of TourDialog and sends it to the business layer
	/// </summary>
	internal class SubmitTourCommand : BaseCommand {
		public TourDialogViewModel TourDialogViewModel { get; }
		public TourListViewModel TourListViewModel { get; }
		public bool IsUpdate { get; }

		public SubmitTourCommand(TourDialogViewModel tourDialogViewModel, TourListViewModel viewModel, bool isUpdate) {
			TourListViewModel = viewModel;
			TourDialogViewModel = tourDialogViewModel;
			IsUpdate = isUpdate; 
		}

		/// <summary>
		/// Checks if all input fields are filled out by the user
		/// </summary>
		/// <returns>True if all input-fields are filled out</returns>
		public override bool CanExecute(object? parameter) {
			return !string.IsNullOrEmpty(TourDialogViewModel.TourDialogName) &&
			       !string.IsNullOrEmpty(TourDialogViewModel.TourDialogDescription) &&
			       !string.IsNullOrEmpty(TourDialogViewModel.TourDialogStart) &&
			       !string.IsNullOrEmpty(TourDialogViewModel.TourDialogDestination) &&
			       base.CanExecute(parameter);
		}

		/// <summary>
		/// Executes Command, send TourData to backend (API Request and create Tour in DB)
		/// </summary>
		public override async void Execute(object? parameter) {
			var tour = new Tour(TourDialogViewModel.TourDialogId,
				TourDialogViewModel.TourDialogName,
				TourDialogViewModel.TourDialogDescription,
				TourDialogViewModel.TourDialogStart,
				TourDialogViewModel.TourDialogDestination,
				TourDialogViewModel.TourDialogTransportType);

			try {
				if (!IsUpdate) {
					tour = await TourDialogViewModel.GetCreatedTour(tour);
					TourListViewModel.AddTour(tour); // Add new Tout to TourListViewModel to update List
				} else {
					tour = await TourDialogViewModel.GetUpdatedTour(tour); 
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

			TourDialogViewModel.CloseAction(); // Close Dialog Window
		}
	}
}