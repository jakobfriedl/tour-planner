using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels; 

public class MainWindowViewModel : BaseViewModel {

	public SearchBarViewModel SearchBarViewModel { get; }
	public MenuStripViewModel MenuStripViewModel { get; }
	public TourDetailsViewModel TourDetailsViewModel { get; }
	public TourListViewModel TourListViewModel { get; }
	public LogListViewModel LogListViewModel { get; }

	public MainWindowViewModel(SearchBarViewModel searchBarViewModel, 
		MenuStripViewModel menuStripViewModel,
		TourListViewModel tourListViewModel, 
		TourDetailsViewModel tourDetailsViewModel,
		LogListViewModel logListViewModel) 
	{
		SearchBarViewModel = searchBarViewModel;
		MenuStripViewModel = menuStripViewModel;
		TourListViewModel = tourListViewModel;
		TourDetailsViewModel = tourDetailsViewModel;
		LogListViewModel = logListViewModel;

		TourListViewModel.Tours.Add(new Tour("Tour 1"));
		TourListViewModel.Tours.Add(new Tour("Tour 2"));
	}
}