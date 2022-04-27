using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
	public class TourListViewModel : BaseViewModel {
		
		public ObservableCollection<Tour> Tours { get; set; }

		public string RouteImageSource { get; set; } = string.Empty; 
		private Tour _selectedTour = null!; 
		public Tour SelectedTour {
			get => _selectedTour;
			set {
				_selectedTour = value ?? new Tour("No Tour selected");
				RouteImageSource = $"{Directory.GetCurrentDirectory()}\\{_selectedTour.ImagePath}";
				OnPropertyChanged(nameof(SelectedTour));
				OnPropertyChanged(nameof(RouteImageSource));
			}
		}

		public ICommand AddTourDialogCommand { get; }
		public ICommand DeleteTourCommand { get; }

		public TourListViewModel() {
			AddTourDialogCommand = new OpenAddTourDialogCommand(this);
			DeleteTourCommand = new DeleteTourCommand(this);
			Tours = new ObservableCollection<Tour>(GetTours()); 
			SelectedTour = Tours.FirstOrDefault()!;
		}

		public void AddTour(Tour tour) {
			Tours.Add(tour);
		}

		public void RemoveSelectedTour() {
			var toRemove = SelectedTour; 
			SelectedTour = Tours.FirstOrDefault()!;
			Tours.Remove(toRemove);
		}

		public bool IsEmpty() {
			return Tours.Count <= 0; 
		}

		public IEnumerable<Tour> GetTours() {
			return ManagerFactory.GetTourManager().GetTours(); 
		}

		public bool DeleteTour() {
			return ManagerFactory.GetTourManager().DeleteTour(SelectedTour.Id); 
		}
	}
}
