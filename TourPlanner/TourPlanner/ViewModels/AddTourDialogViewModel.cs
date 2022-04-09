using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
    public class AddTourDialogViewModel {
	    public ICommand SubmitCommand { get; }

	    public string? AddTourName { get; set; } = "Name";
	    public string? AddTourDescription { get; set; } = "Description";
	    public string? AddTourStart { get; set; } = "Start";
	    public string? AddTourDestination { get; set; } = "Destination";
	    public TransportType AddTourTransportType { get; set; } = TransportType.Car;

        public AddTourDialogViewModel() {
	        SubmitCommand = new SubmitTourCommand(this);
        }
    }
}
