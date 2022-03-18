﻿using System;
using System.Collections.Generic;
using System.Linq;
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

	    public RelayCommand(Action<object> executeAction, Func<object, bool>? canExecuteAction = null) {
	        ExecuteAction = executeAction;
	        CanExecuteAction = canExecuteAction;
        }
    }
}
