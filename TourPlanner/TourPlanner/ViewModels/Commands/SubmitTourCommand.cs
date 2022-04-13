using System.Windows;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands; 

/// <summary>
///     Button which validates input of AddTourDialog and sends it to the business layer
/// </summary>
internal class SubmitTourCommand : BaseCommand {
	public AddTourDialogViewModel Data { get; }
	public TourListViewModel TourListViewModel { get; }

	public SubmitTourCommand(TourListViewModel vm, AddTourDialogViewModel data) {
		TourListViewModel = vm; 
		Data = data;
	}

	/// <summary>
	/// Checks if all input fields are filled out by the user
	/// </summary>
	/// <param name="parameter"></param>
	/// <returns></returns>
	public override bool CanExecute(object? parameter) {
		return !string.IsNullOrEmpty(Data.AddTourName) &&
		       !string.IsNullOrEmpty(Data.AddTourDescription) &&
		       !string.IsNullOrEmpty(Data.AddTourStart) &&
		       !string.IsNullOrEmpty(Data.AddTourDestination) &&
		       base.CanExecute(parameter);
	}

	/// <summary>
	/// Executes Command, send TourData to backend (API Request and create Tour in DB)
	/// </summary>
	/// <param name="parameter"></param>
	public override async void Execute(object? parameter) {
		var tour = new Tour(Data.AddTourName,
							Data.AddTourDescription,
							Data.AddTourStart,
							Data.AddTourDestination,
							Data.AddTourTransportType);

		tour = await ManagerFactory.GetTourManager().CreateTour(tour);

		if (tour is null) { 
			// Invalid Location
			MessageBox.Show("Please make sure to enter valid locations.",
				"Invalid locations",
				MessageBoxButton.OK, 
				MessageBoxImage.Error);
		} else {
			TourListViewModel.AddTour(tour); // Add new Tout to TourListViewModel to update List
			Data.CloseAction(); // Close Dialog Window}
		}
	}
}