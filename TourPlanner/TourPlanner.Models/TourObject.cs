using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TourPlanner.Models
{
    public class TourObject
    {
        public TourObject(Tour tour, string image, ObservableCollection<Log> logs)
        {
            Tour = tour;
            ImageInBase64 = image;
            Logs = logs;
        }

        [JsonProperty("Tour")]
        public Tour Tour { get; set; }

        [JsonProperty("Image")]
        public string ImageInBase64 { get; set; }

        [JsonProperty("Logs")]
        public ObservableCollection<Log> Logs { get; set; } = new ObservableCollection<Log>();
    }
}
