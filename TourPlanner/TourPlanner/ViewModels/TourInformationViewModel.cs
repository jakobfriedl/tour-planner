using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class TourInformationViewModel
    {
	    public TourListViewModel TourListViewModel { get; }
	    public Tour SelectedTour { get; }

	    public TourInformationViewModel(TourListViewModel tourListViewModel) {
		    TourListViewModel = tourListViewModel;
		    SelectedTour = TourListViewModel.SelectedTour; 
	    }
    }
}
