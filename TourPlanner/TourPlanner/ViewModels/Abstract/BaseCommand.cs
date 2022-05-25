using System;
using System.Windows.Input;

namespace TourPlanner.ViewModels.Abstract
{
    public abstract class BaseCommand : ICommand
    {
	    protected Predicate<object?>? CanExecuteAction;

	    public event EventHandler? CanExecuteChanged {
		    add => CommandManager.RequerySuggested += value;
		    remove => CommandManager.RequerySuggested -= value;
	    }

	    public virtual bool CanExecute(object? parameter) {
		    return CanExecuteAction is null || CanExecute(parameter);
	    }

	    public abstract void Execute(object? parameter); 
    }
}
