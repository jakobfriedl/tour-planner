using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
	public class SearchBarViewModel : BaseViewModel
    {
		public ICommand SearchCommand { get; }
		public string SearchTerm { get; set; } = string.Empty; 

		public SearchBarViewModel(TourListViewModel tourListViewModel) {
			SearchCommand = new RelayCommand((_) => {
				tourListViewModel.Tours = new ObservableCollection<Tour>(SearchTours());
			});
		}

		public IEnumerable<Tour> SearchTours() {
			return ManagerFactory.GetTourManager().SearchTours(SearchTerm); 
		}
    }
}
