using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands; 

/// <summary>
///     Button which validates input of AddTourDialog and sends it to the business layer
/// </summary>
internal class SubmitTourCommand : BaseCommand {
	public SubmitTourCommand(AddTourDialogViewModel data) {
		Data = data;
	}

	public AddTourDialogViewModel Data { get; set; }

	public override bool CanExecute(object? parameter) {
		return !string.IsNullOrEmpty(Data.AddTourName) &&
		       !string.IsNullOrEmpty(Data.AddTourDescription) &&
		       !string.IsNullOrEmpty(Data.AddTourStart) &&
		       !string.IsNullOrEmpty(Data.AddTourDestination) &&
		       base.CanExecute(parameter);
	}

	public override void Execute(object? parameter) {
		var tour = new Tour(Data.AddTourName, Data.AddTourDescription, Data.AddTourStart, Data.AddTourDestination,
			Data.AddTourTransportType);
		tour = ManagerFactory.GetTourManager().CreateTour(tour);

		Data.CloseAction(); 
	}
}