using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
	public class SearchBarViewModel : BaseViewModel
    {
		public string? SearchTerm { get; set; }

		public SearchBarViewModel() {
			SearchTerm = "Search Data Binding"; 
		}
    }
}
