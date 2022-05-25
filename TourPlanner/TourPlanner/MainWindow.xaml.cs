using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
	    public MainWindow(MainWindowViewModel viewModel) {
		    DataContext = viewModel; 
            InitializeComponent();
        }
    }
}
