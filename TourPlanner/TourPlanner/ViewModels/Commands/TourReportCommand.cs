using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands {
	public class TourReportCommand : BaseCommand {
		private readonly ILogger _logger;
		public TourListViewModel TourListViewModel { get; set; }
		public LogListViewModel LogListViewModel { get; set; }

		private readonly string _reportLocation;

		public TourReportCommand(ILogger logger, TourListViewModel tourListViewModel, LogListViewModel logListViewModel) {
			_logger = logger;
			TourListViewModel = tourListViewModel;
			LogListViewModel = logListViewModel;
			_reportLocation = $"{Directory.GetCurrentDirectory()}\\Resources\\reports";
		}

		/// <summary>
		/// Executes the PDF Report Generation, if successfully the generated PDF will open in the chosen pdf viewer of the host
		/// </summary>
		/// <param name="parameter"></param>
		public override void Execute(object? parameter) {
			var filename = Path.Combine(_reportLocation, TourListViewModel.SelectedTour.Id.ToString() + ".pdf");
			var tourReport = new TourReportGenerator();
			tourReport.CreateTourReport(_logger, TourListViewModel.SelectedTour, LogListViewModel.Logs);
			if (File.Exists(filename)) {
				var p = new Process();
				p.StartInfo.FileName = filename;
				p.StartInfo.UseShellExecute = true;
				p.Start();
			} else {
				MessageBox.Show("Report could not be created", "Report creation Error", MessageBoxButton.OK,
					MessageBoxImage.Error);
			}
		}
	}
}