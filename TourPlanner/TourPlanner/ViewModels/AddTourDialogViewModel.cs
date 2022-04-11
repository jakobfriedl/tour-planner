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

	    public string AddTourName { get; set; }
	    public string AddTourDescription { get; set; }
	    public string AddTourStart { get; set; }
	    public string AddTourDestination { get; set; }
	    public TransportType AddTourTransportType { get; set; }

        public AddTourDialogViewModel(Action closeAction) {
	        SubmitCommand = new SubmitTourCommand(this);
	        CloseAction = closeAction; 
        }
    }
}
