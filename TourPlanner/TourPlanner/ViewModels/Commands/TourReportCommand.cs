using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace TourPlanner.ViewModels.Commands
{
    public class TourReportCommand : BaseCommand
    {

        public TourListViewModel TourListViewModel { get; set; }
        public LogListViewModel LogListViewModel { get; set; }

        private readonly string _reportLocation;

        public TourReportCommand(TourListViewModel tourListViewModel, LogListViewModel logListViewModel)
        {
            TourListViewModel = tourListViewModel;
            LogListViewModel = logListViewModel;
            _reportLocation = $"{Directory.GetCurrentDirectory()}\\Resources\\reports";
        }

        /// <summary>
        /// Executes the PDF Report Generation, if successfully the generated PDF will open in the chosen pdf viewer of the host
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            var filename = Path.Combine(_reportLocation, TourListViewModel.SelectedTour.Id.ToString() + ".pdf");
            var tourReport = new TourReportGeneration();
            tourReport.TourReportGenerator(TourListViewModel.SelectedTour, LogListViewModel.Logs);
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