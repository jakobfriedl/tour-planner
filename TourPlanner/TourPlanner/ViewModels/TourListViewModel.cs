using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
	public class TourListViewModel : BaseViewModel {
		
		public ICommand AddTourDialogCommand { get; }
		public ICommand EditTourDialogCommand { get; set; }
		public ICommand DeleteTourCommand { get; }

		public ObservableCollection<Tour> Tours { get; set; }

		public LogListViewModel LogListViewModel { get; set; }

		public byte[]? RouteImageSource { get; set; } 
		private Tour _selectedTour = null!; 
		public Tour SelectedTour {
			get => _selectedTour;
			set {
				_selectedTour = value ?? new Tour("No Tour selected");
				OnPropertyChanged(nameof(SelectedTour));

				if (_selectedTour.ImagePath is not null) {
					var path = Path.Combine(Directory.GetCurrentDirectory(), _selectedTour.ImagePath);
					using (var stream = File.Open(path, FileMode.Open)) {
						RouteImageSource = ReadFully(stream);
					}
					OnPropertyChanged(nameof(RouteImageSource));
				}

				EditTourDialogCommand = new OpenEditTourDialogCommand(this);
				OnPropertyChanged(nameof(EditTourDialogCommand));

				LogListViewModel.Init(SelectedTour);
			}
		}

		public static byte[] ReadFully(Stream input) {
			var buffer = new byte[16 * 1024];
			using (var ms = new MemoryStream()) {
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public TourListViewModel(LogListViewModel logListViewModel) {
			LogListViewModel = logListViewModel;
			LogListViewModel.Init(SelectedTour);

			Tours = new ObservableCollection<Tour>(GetTours());
			SelectedTour = Tours.FirstOrDefault()!;

			AddTourDialogCommand = new RelayCommand((_) => {
				var dialog = new TourDialog(this);
				dialog.ShowDialog();
			});
			EditTourDialogCommand = new OpenEditTourDialogCommand(this);
			DeleteTourCommand = new DeleteTourCommand(this);
		}

		public void AddTour(Tour tour) {
			Tours.Add(tour);
		}

		public void RemoveSelectedTour() {
			var toRemove = SelectedTour; 
			SelectedTour = Tours.FirstOrDefault()!;
			Tours.Remove(toRemove);
		}

		public void ReplaceTour(Tour tour) {
			var t = Tours.FirstOrDefault(t => t.Id == tour.Id);
			Tours[Tours.IndexOf(t)] = tour;
			SelectedTour = tour; 
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
