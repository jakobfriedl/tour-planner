using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class ExportToursCommand : BaseCommand
    {
        ExportTours exportTours = new ExportTours();
        private readonly string _exportLocation;

        public ExportToursCommand(string exportLocation)
        {
            _exportLocation = exportLocation;
        }
        public ExportToursCommand()
        {
            _exportLocation = $"{Directory.GetCurrentDirectory()}\\Resources\\exports";
        }

        public override void Execute(object? parameter)
        {
            exportTours.Export(_exportLocation);
        }
    }
}
