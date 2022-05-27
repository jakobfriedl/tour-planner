using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TourPlanner.BusinessLayer
{
    public class ExportTours
    {
        private readonly ILogger _logger;

        public ExportTours(ILogger logger)
        {
            _logger = logger;
        }

        public void Export()
        {
            var location = $"{Directory.GetCurrentDirectory()}\\Resources\\exports"; 
            var exportJson = $"{location}\\exportTours.json";
            var application = "explorer.exe";

            TourManager tourManager = new TourManager(_logger);
            LogManager logManager = new LogManager(_logger);
            Models.JSON.TourObjectsCollection tourObjectsCollection = new Models.JSON.TourObjectsCollection();

            List<Tour> Tours = new List<Tour>(tourManager.GetTours());

            if (!Tours.Any())
            {
                _logger.LogWarning($"No Tours to export. {DateTime.UtcNow}");
                MessageBox.Show("You have no Tours to export.", "No Tours found", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            if (!Directory.Exists(location))
            {
                _logger.LogWarning($"Directory does not exist: {location}. {DateTime.UtcNow}");
                Directory.CreateDirectory(location);
            }

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "json file(*.json)| *.json";
            openFileDialog.Title = "Save the Export as JSON";
            openFileDialog.ShowDialog();
            exportJson = openFileDialog.FileName;

            StreamWriter jsonFile = new StreamWriter(exportJson);
            foreach (Tour t in Tours)
            {
                byte[] imageArray = File.ReadAllBytes(t.ImagePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                TourObject tourObject = new TourObject(t, base64ImageRepresentation, new List<Log>(logManager.GetLogs(t.Id)));
                tourObjectsCollection.TourObjects.Add(tourObject);
            }
            string jsonString = JsonConvert.SerializeObject(tourObjectsCollection, Formatting.Indented);
            jsonFile.WriteLine(jsonString);

            jsonFile.Flush();

            if (Directory.Exists(location))
            {
                var startInfo = new ProcessStartInfo(application, location);
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show($"Exporting the Tours was not successful.", "Tours Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
