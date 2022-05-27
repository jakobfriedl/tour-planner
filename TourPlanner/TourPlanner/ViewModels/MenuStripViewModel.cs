using System.Windows.Input;
using Microsoft.Extensions.Logging;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class MenuStripViewModel : BaseViewModel
    {
        public ICommand OpenSettingsCommand { get; }
        public ICommand ExportToursCommand { get; }
        public ICommand ImportToursCommand { get; }
        public ICommand TourReportCommand { get; }
        public ICommand SummarizeReportCommand { get; }
        
        public MenuStripViewModel(ILogger logger, TourListViewModel tourListViewModel, LogListViewModel logListViewModel)
        {
            OpenSettingsCommand = new OpenSettingsCommand();
            ExportToursCommand = new ExportToursCommand();
            ImportToursCommand = new RelayCommand(_ =>
            {
                var import = new ImportTours();
                import.Import();
                tourListViewModel.Tours = new ObservableCollection<Tour>(tourListViewModel.GetTours());
                OnPropertyChanged(nameof(TourListViewModel));
            });
            TourReportCommand = new TourReportCommand(logger,tourListViewModel, logListViewModel);
            SummarizeReportCommand = new SummarizeReportCommand(logger, tourListViewModel, logListViewModel);
        }
    }
}