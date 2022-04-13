using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccessLayer.Config
{
    public class TourPlannerConfig
    {
        public string? ImageLocation { get; set; }
        public string? DatabaseHost { get; set; }
        public string? DatabaseUsername { get; set; }
        public string? DatabasePassword { get; set; }
        public string? DatabaseName { get; set; }
        public string? ApiKey { get; set; }
    }
}
