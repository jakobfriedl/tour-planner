using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
	public class TourListViewModel : BaseViewModel {
		
		public ObservableCollection<Tour> Tours { get; set; } = new();

		private Tour _selectedTour = null!; 
		public Tour SelectedTour {
			get => _selectedTour;
			set {
				_selectedTour = value;
				OnPropertyChanged(nameof(SelectedTour));
			}
		}

		public ICommand AddTourDialogCommand { get; }

		public TourListViewModel() {
			AddTourDialogCommand = new OpenAddTourDialogCommand(this);
			AddTour(new Tour(1, "Tour 1",
				"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
				"asd", "asd,", TransportType.Bike, 100, 1000.0, "asd", 3, 4));
			AddTour(new Tour(2, "Tour 2",
				"Lorem ipsum dolor sit amet, consectetur adipiscing elit,  consequat.",
				"asdasd", "asasdd,", TransportType.Bike, 100, 1000.0, "asd", 3, 4));
			SelectedTour = Tours.First();
		}

		public void AddTour(Tour tour) {
			Tours.Add(tour);
		}
	}
}
