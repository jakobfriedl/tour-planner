using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class MenuStripViewModel : BaseViewModel
    {
        public ICommand TourReportCommand { get; }
        public ICommand SummarizeReportCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public MenuStripViewModel(TourListViewModel tourListViewModel, LogListViewModel logListViewModel)
        {
            TourReportCommand = new TourReportCommand(tourListViewModel, logListViewModel);
            SummarizeReportCommand = new SummarizeReportCommand(tourListViewModel, logListViewModel);
            OpenSettingsCommand = new OpenSettingsCommand();
        }
    }
}