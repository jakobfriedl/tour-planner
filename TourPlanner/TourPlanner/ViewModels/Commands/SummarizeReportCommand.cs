using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer.ReportGeneration;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands {
	public class SummarizeReportCommand : BaseCommand {

		private readonly ILogger _logger; 

		public TourListViewModel TourListViewModel { get; set; }
		public LogListViewModel LogListViewModel { get; set; }

		private readonly string _reportLocation;

		public SummarizeReportCommand(ILogger logger, TourListViewModel tourListViewModel, LogListViewModel logListViewModel) {
			_logger = logger; 
			TourListViewModel = tourListViewModel;
			LogListViewModel = logListViewModel;
			_reportLocation = $"{Directory.GetCurrentDirectory()}\\Resources\\reports";
		}

		public override void Execute(object? parameter) {
			var filename = Path.Combine(_reportLocation, "summarized.pdf");
			var summarizeReport = new SummarizeReportGenerator();
			summarizeReport.CreateSummarizeReport(_logger, TourListViewModel.Tours, LogListViewModel.Logs);

			if (File.Exists(filename)) {
				var ps = new Process();
				ps.StartInfo.FileName = filename;
				ps.StartInfo.UseShellExecute = true;
				ps.Start();
			} else {
				MessageBox.Show("Report could not be created", "Report creation Error", MessageBoxButton.OK,
					MessageBoxImage.Error);
			}
		}
	}
}