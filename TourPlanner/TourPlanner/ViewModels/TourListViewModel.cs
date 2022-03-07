using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
	public class TourListViewModel : BaseViewModel {
	    public ObservableCollection<Tour> Tours { get; set; } = new();
    }
}
