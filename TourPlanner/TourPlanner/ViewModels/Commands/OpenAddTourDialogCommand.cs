using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.Views;

namespace TourPlanner.ViewModels.Commands
{
	/// <summary>
	/// Specific Command for opening the Add-Tour dialog/modal window
	/// </summary>
	public class OpenAddTourDialogCommand : BaseCommand {
		public TourListViewModel TourListViewModel { get; }

		public OpenAddTourDialogCommand(TourListViewModel vm) {
			TourListViewModel = vm;
		}

		public override void Execute(object? parameter) {
		    var dialog = new TourDialog(TourListViewModel);
			dialog.ShowDialog(); 
		}
	}
}
