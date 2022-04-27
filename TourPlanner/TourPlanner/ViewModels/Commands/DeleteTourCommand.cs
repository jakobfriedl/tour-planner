using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands {

	public class DeleteTourCommand : BaseCommand {
		public TourListViewModel TourListViewModel { get; }

		public DeleteTourCommand(TourListViewModel tourListViewModel) {
			TourListViewModel = tourListViewModel; 
		}

		public override bool CanExecute(object? parameter) {
			return !TourListViewModel.IsEmpty() &&
				base.CanExecute(parameter);
		}

		public override void Execute(object? parameter) {
			if (!TourListViewModel.DeleteTour()) {
				MessageBox.Show("Could not delete selected tour", "Tour deletion error", MessageBoxButton.OK,
					MessageBoxImage.Error); 
				return;
			}
			TourListViewModel.RemoveSelectedTour();
		}
	}
}