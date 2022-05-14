using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.BusinessLayer;
using System.Windows;
using System.Diagnostics;

namespace TourPlanner.ViewModels.Commands
{
    public class SummarizeReportCommand : BaseCommand
    {
        public TourListViewModel TourListViewModel { get; set; }
        public LogListViewModel LogListViewModel { get; set; }

        private readonly string _reportLocation;

        public SummarizeReportCommand(TourListViewModel tourListViewModel, LogListViewModel logListViewModel)
        {
            TourListViewModel = tourListViewModel;
            LogListViewModel = logListViewModel;
            _reportLocation = $"{Directory.GetCurrentDirectory()}\\Resources\\reports";
        }
        public override void Execute(object? parameter)
        {
            var filename = Path.Combine(_reportLocation, "summarized.pdf");
            var summarizeReport = new SummarizeReportGeneration();
            summarizeReport.SummarizeReportGenerator(TourListViewModel.Tours, LogListViewModel.Logs);
            if (File.Exists(filename))
            {
                var p = new Process();
                p.StartInfo.FileName = filename;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }
            else
            {
                MessageBox.Show("Report could not be created", "Report creation Error", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

        }
    }
}