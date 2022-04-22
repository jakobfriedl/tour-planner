using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
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
