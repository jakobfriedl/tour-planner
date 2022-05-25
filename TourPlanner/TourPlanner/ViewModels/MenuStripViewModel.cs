using System.Windows.Input;
using Microsoft.Extensions.Logging;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class MenuStripViewModel : BaseViewModel
    {
        public ICommand TourReportCommand { get; }
        public ICommand SummarizeReportCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public MenuStripViewModel(ILogger logger, TourListViewModel tourListViewModel, LogListViewModel logListViewModel) {
            TourReportCommand = new TourReportCommand(logger, tourListViewModel, logListViewModel);
            SummarizeReportCommand = new SummarizeReportCommand(logger, tourListViewModel, logListViewModel);
            OpenSettingsCommand = new OpenSettingsCommand();
        }
    }
}