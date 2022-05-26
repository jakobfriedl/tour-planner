using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.JSON
{
    public class TourObjectsCollection
    {
        public List<TourObject> TourObjects { get; set; } = new();
    }
}
