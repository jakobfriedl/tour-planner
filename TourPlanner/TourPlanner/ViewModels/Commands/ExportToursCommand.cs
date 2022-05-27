using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class ExportToursCommand : BaseCommand
    {
        private readonly ILogger _logger;

        public ExportToursCommand(ILogger logger)
        {
            _logger = logger;
        }

        public override void Execute(object? parameter)
        {
            ExportTours exportTours = new ExportTours(_logger);
            exportTours.Export();
        }
    }
}
