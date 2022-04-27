﻿using System;
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

		public SubmitTourCommand(TourListViewModel viewModel, AddTourDialogViewModel addTourDialogViewModel) {
			TourListViewModel = viewModel;
			AddTourDialogViewModel = addTourDialogViewModel;
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
			var tour = new Tour(AddTourDialogViewModel.AddTourName,
				AddTourDialogViewModel.AddTourDescription,
				AddTourDialogViewModel.AddTourStart,
				AddTourDialogViewModel.AddTourDestination,
				AddTourDialogViewModel.AddTourTransportType);

			try {
				tour = await AddTourDialogViewModel.GetCreatedTour(tour);
			} catch (InvalidLocationException) {
				// Invalid Location, show Error-MessageBox
				MessageBox.Show("Please make sure to enter valid locations.",
					"Invalid locations",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			TourListViewModel.AddTour(tour); // Add new Tout to TourListViewModel to update List
			AddTourDialogViewModel.CloseAction(); // Close Dialog Window
		}
	}
}