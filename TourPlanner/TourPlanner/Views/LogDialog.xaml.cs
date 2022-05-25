using System.Windows;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for LogDialog.xaml
    /// </summary>
    public partial class LogDialog : Window
    {
        public LogDialog(LogListViewModel logListViewModel, Tour selectedTour, Log? logToUpdate = null) {
	        DataContext = new LogDialogViewModel(logListViewModel, selectedTour, logToUpdate, this.Close); 
            InitializeComponent();
        }
    }
}
