using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
	public class MenuStripViewModel : BaseViewModel
    {
        public ICommand OpenSettingsCommand { get; }

        public MenuStripViewModel() {
	        OpenSettingsCommand = new OpenSettingsCommand(); 
        }
    }
}
