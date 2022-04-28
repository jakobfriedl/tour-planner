using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
	public class LogListViewModel : BaseViewModel
    {
        public ICommand AddLogDialogCommand { get; }
        public ICommand EditLogDialogCommand { get; }
        public ICommand DeleteLogCommand { get; }

        public ObservableCollection<Log> Logs { get; set; } = new();

        public LogListViewModel(TourListViewModel tourListViewModel) {
	        AddLogDialogCommand = new OpenAddLogDialogCommand(this, tourListViewModel);

            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 91, DateTime.Now, 300, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 92, DateTime.Now, 500, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 93, DateTime.Now, 200, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 400, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What aasd nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What a nice tour", 3, 5));
            Logs.Add(new Log(0, 90, DateTime.Now, 300, "What a nice tour", 3, 5));
        }
    }
}
