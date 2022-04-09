using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.BusinessLayer;

namespace TourPlanner.ViewModels.Commands
{
	/// <summary>
	/// Button which validates input of AddTourDialog and sends it to the business layer
	/// </summary>
    internal class SubmitTourCommand : BaseCommand
    {
		public AddTourDialogViewModel Data { get; set; }

	    public SubmitTourCommand(AddTourDialogViewModel data) {
		    Data = data;
	    }

	    public override bool CanExecute(object? parameter) {
		    return !string.IsNullOrEmpty(Data.AddTourName) &&
		           !string.IsNullOrEmpty(Data.AddTourDescription) &&
		           !string.IsNullOrEmpty(Data.AddTourStart) &&
		           !string.IsNullOrEmpty(Data.AddTourDestination) &&
					base.CanExecute(parameter);
	    }
	    
	    public override void Execute(object? parameter) {
		    throw new NotImplementedException(); 
	    }
    }
}
