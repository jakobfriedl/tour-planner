using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
	    public ICommand AddLogDialogCommand { get; set; }
        public ICommand EditLogDialogCommand { get; }
        public ICommand DeleteLogCommand { get; }

        public string LogListHeading { get; set; }
        public ObservableCollection<Log> Logs { get; set; }

        public void Init(Tour selectedTour) {
	        if (selectedTour == null) return; 

	        AddLogDialogCommand = new RelayCommand((_) => {
		        var dialog = new LogDialog(this, selectedTour);
		        dialog.ShowDialog();
            });

	        Logs = new ObservableCollection<Log>(GetLogs(selectedTour));
            OnPropertyChanged(nameof(Logs));
	        LogListHeading = $"Logs for \"{selectedTour.Name}\""; 
            OnPropertyChanged(nameof(LogListHeading));
        }

        public void AddLog(Log log) {
            Logs.Add(log);
        }

        private IEnumerable<Log> GetLogs(Tour selectedTour) {
            return ManagerFactory.GetLogManager().GetLogs(selectedTour.Id);
        }
    }
}
