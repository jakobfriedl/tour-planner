using System.Windows;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for TourDialog.xaml
    /// </summary>
    public partial class TourDialog : Window
    {
        public TourDialog(TourListViewModel viewModel, Tour? tourToUpdate = null!) {
	        DataContext = new TourDialogViewModel(viewModel, this.Close, tourToUpdate);
	        InitializeComponent();
        }
    }
}
