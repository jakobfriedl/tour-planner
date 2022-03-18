using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Abstract
{
    public abstract class BaseCommand : ICommand
    {
	    protected Action<object> ExecuteAction;
	    protected Func<object, bool>? CanExecuteAction;

	    public event EventHandler? CanExecuteChanged {
		    add => CommandManager.RequerySuggested += value;
		    remove => CommandManager.RequerySuggested -= value;
	    }

	    public bool CanExecute(object? parameter) {
		    return CanExecuteAction is null || CanExecute(parameter);
	    }

	    public void Execute(object? parameter) {
		    ExecuteAction(parameter!);
	    }
    }
}
