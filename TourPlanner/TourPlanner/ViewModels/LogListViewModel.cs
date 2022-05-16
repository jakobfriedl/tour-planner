using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
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
	    public ICommand? AddLogDialogCommand { get; set; }
        public ICommand? DeleteLogCommand { get; set; }
        public ICommand? EditLogDialogCommand { get; set; }

        public Tour SelectedTour { get; set; } = null!; 

	    private Log _selectedLog = null!;
        public Log SelectedLog {
	        get => _selectedLog;
	        set {
		        _selectedLog = value;
                OnPropertyChanged(nameof(SelectedLog));

                DeleteLogCommand = new DeleteLogCommand(this); 
                OnPropertyChanged(nameof(DeleteLogCommand));

                EditLogDialogCommand = new OpenEditLogDialogCommand(this, SelectedTour); 
                OnPropertyChanged(nameof(EditLogDialogCommand));
	        }
        }

        public string LogListHeading { get; set; } = "Logs";
        public ObservableCollection<Log> Logs { get; set; } = new();

        public void UpdateView(Tour? selectedTour) {
	        if (selectedTour == null) return;
	        SelectedTour = selectedTour; 

	        AddLogDialogCommand = new RelayCommand((_) => {
		        var dialog = new LogDialog(this, selectedTour);
		        dialog.ShowDialog();
	        });
	        OnPropertyChanged(nameof(AddLogDialogCommand));

			Logs = new ObservableCollection<Log>(GetLogs(selectedTour));
            OnPropertyChanged(nameof(Logs));
            SelectedLog = Logs.FirstOrDefault()!; 

	        LogListHeading = $"Logs for \"{selectedTour.Name}\"";
			OnPropertyChanged(nameof(LogListHeading));

			EditLogDialogCommand = new OpenEditLogDialogCommand(this, selectedTour);
			OnPropertyChanged(nameof(EditLogDialogCommand));
        }

        public bool IsEmpty() {
	        return Logs.Count <= 0; 
        }

        public void AddLog(Log log) {
            Logs.Add(log);
            SelectedLog = log; 
        }

        public void RemoveSelectedLog() {
	        var toRemove = SelectedLog;
	        SelectedLog = Logs.FirstOrDefault()!;
	        Logs.Remove(toRemove);
        }

        public void ReplaceLog(Log log) {
	        var l = Logs.FirstOrDefault(l => l.Id == log.Id);
	        Logs[Logs.IndexOf(l)] = log;
	        SelectedLog = log;
        }
        
        private IEnumerable<Log> GetLogs(Tour selectedTour) {
            return ManagerFactory.GetLogManager().GetLogs(selectedTour.Id);
        }
        
        public bool DeleteLog() {
	        var r = ManagerFactory.GetLogManager().DeleteLog(SelectedLog.Id);
	        UpdateStats();
	        return r; 
        }

        public Log GetCreatedLog(Log log) {
	        log = ManagerFactory.GetLogManager().CreateLog(log);
	        UpdateStats();
	        return log;
        }

        public Log GetUpdatedLog(Log log) {
	        log = ManagerFactory.GetLogManager().UpdateLog(log);
	        UpdateStats();
	        return log;
        }

        private void UpdateStats() {
	        SelectedTour.Popularity = ManagerFactory.GetStatManager().GetPopularity(SelectedTour.Id);
	        SelectedTour.ChildFriendliness = ManagerFactory.GetStatManager().GetChildFriendliness(SelectedTour.Id);
	        OnPropertyChanged(nameof(SelectedTour));
        }
	}
}
