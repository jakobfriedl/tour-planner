using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class AddTourDialogViewModel : BaseViewModel {
		public Action CloseAction { get; }
	    public ICommand SubmitCommand { get; }

	    public string AddTourName { get; set; } = string.Empty;
		public string AddTourDescription { get; set; } = string.Empty;
		public string AddTourStart { get; set; } = string.Empty;
		public string AddTourDestination { get; set; } = string.Empty;
		public TransportType AddTourTransportType { get; set; }

        public AddTourDialogViewModel(TourListViewModel viewModel, Action closeAction) {
	        SubmitCommand = new SubmitTourCommand(viewModel, this);
	        CloseAction = closeAction; 
        }
    }
}