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

		public ICommand AddTourDialogCommand { get; }

		public TourListViewModel() {
			// Template value determines the type of dialog
			AddTourDialogCommand = new OpenAddTourDialogCommand(this);
			AddTour(new Tour("Tour 1"));
			AddTour(new Tour("Tour 2"));
		}

		public void AddTour(Tour tour) {
			Tours.Add(tour);
		}
	}
}
