using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
	public class TourDetailsViewModel : BaseViewModel
    {
        public TourListViewModel TourListViewModel { get; }

        public TourDetailsViewModel(TourListViewModel tourListViewModel) {
	        TourListViewModel = tourListViewModel;
        }
    }
}
