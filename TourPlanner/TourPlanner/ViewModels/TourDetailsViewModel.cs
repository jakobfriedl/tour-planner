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
        public TourInformationViewModel TourInformationViewModel { get; }

        public TourDetailsViewModel(TourInformationViewModel tourInformationViewModel) {
	        TourInformationViewModel = tourInformationViewModel;
        }
    }
}
