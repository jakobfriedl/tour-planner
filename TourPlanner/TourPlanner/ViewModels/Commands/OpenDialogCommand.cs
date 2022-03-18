using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.Views;

namespace TourPlanner.ViewModels.Commands
{
	/// <summary>
	/// Specific Command for opening dialog/modal windows
	/// </summary>
	/// <typeparam name="T"> Type of dialog (e.g. AddTourDialog, AddLogDialog, EditTourDialog) </typeparam>
    public class OpenDialogCommand<T> : BaseCommand where T : Window, new() {

	    public OpenDialogCommand(Func<object, bool>? canExecuteAction = null) {
		    ExecuteAction = _ => {
			    var dialog = new T(); 
			    dialog.ShowDialog();
		    };
		    CanExecuteAction = canExecuteAction;
	    }
    }
}
