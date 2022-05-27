using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
	public class SearchBarViewModel : BaseViewModel
    {
		private readonly ILogger _logger;
		public ICommand SearchCommand { get; }
		public string SearchTerm { get; set; } = string.Empty; 

		public SearchBarViewModel(ILogger logger, TourListViewModel tourListViewModel) {
			_logger = logger; 
			SearchCommand = new RelayCommand((_) => {
				tourListViewModel.Tours = new ObservableCollection<Tour>(SearchTours());
			});
		}

		public IEnumerable<Tour> SearchTours() {
			return ManagerFactory.GetTourManager(_logger).SearchTours(SearchTerm); 
		}
    }
}
