using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.Commands
{
    public class ImportToursCommand : BaseCommand
    {
        ImportTours importTours = new ImportTours();

        public override void Execute(object? parameter)
        {
            importTours.Import();
        }
    }
}
