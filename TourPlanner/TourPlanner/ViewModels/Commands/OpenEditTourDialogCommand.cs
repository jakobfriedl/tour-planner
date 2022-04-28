using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.Views;

namespace TourPlanner.ViewModels.Commands {
	public class OpenEditTourDialogCommand : BaseCommand {
		public TourListViewModel TourListViewModel { get; }
		public Tour TourToUpdate { get; set; }

		public OpenEditTourDialogCommand(TourListViewModel vm) {
			TourListViewModel = vm;
			TourToUpdate = TourListViewModel.SelectedTour; 
		}

		public override bool CanExecute(object? parameter) {
			return !TourListViewModel.IsEmpty() && 
				base.CanExecute(parameter);
		}

		public override void Execute(object? parameter) {
			var dialog = new TourDialog(TourListViewModel, TourToUpdate);
			dialog.ShowDialog();
		}
	}
}