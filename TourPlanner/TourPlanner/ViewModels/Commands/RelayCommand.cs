using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    /// <summary>
    /// Basic command that can be given functionality with delegate expressions
    /// </summary>
    public class RelayCommand : BaseCommand {
        public Action<object?> ExecuteAction { get; set; }

	    public RelayCommand(Action<object?> executeAction, Predicate<object?>? canExecuteAction = null) {
	        ExecuteAction = executeAction;
	        CanExecuteAction = canExecuteAction;
        }

	    public override void Execute(object? parameter) {
		    ExecuteAction(parameter); 
	    }
    }
}
