using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;
using TourPlanner.Views;

namespace TourPlanner.ViewModels
{
	public class LogListViewModel : BaseViewModel
    {
	    public ICommand AddLogDialogCommand { get; }
        public ICommand EditLogDialogCommand { get; }
        public ICommand DeleteLogCommand { get; }

        public string LogListHeading { get; set; }
        public ObservableCollection<Log> Logs { get; set; }

        public LogListViewModel(TourListViewModel tourListViewModel) {
	        AddLogDialogCommand = new RelayCommand((_) => {
		        var dialog = new LogDialog(this, tourListViewModel);
		        dialog.ShowDialog();
            }); 

	        Logs = new ObservableCollection<Log>(GetLogs(tourListViewModel));
	        LogListHeading = $"Logs for \"{tourListViewModel.SelectedTour.Name}\""; 
        }

        public void AddLog(Log log) {
            Logs.Add(log);
        }

        private IEnumerable<Log> GetLogs(TourListViewModel tourListViewModel) {
            return ManagerFactory.GetLogManager().GetLogs(tourListViewModel.SelectedTour.Id);
        }
    }
}
