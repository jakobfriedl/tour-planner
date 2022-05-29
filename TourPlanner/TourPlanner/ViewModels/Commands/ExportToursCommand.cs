using Microsoft.Extensions.Logging;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class ExportToursCommand : BaseCommand
    {
        private readonly ILogger _logger;

        public ExportToursCommand(ILogger logger) {
            _logger = logger;
        }

        public override void Execute(object? parameter) {
            var exportTours = new ExportTours(_logger);
            exportTours.Export();
        }
    }
}
