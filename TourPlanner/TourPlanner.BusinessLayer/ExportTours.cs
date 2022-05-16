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
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TourPlanner.BusinessLayer
{
    public class ExportTours
    {
        public void Export(string location)
        {
            var exportJson = $"{location}\\exportTours.json";
            var application = "explorer.exe";

            TourManager tourManager = new TourManager();
            LogManager logManager = new LogManager();
            TourObjectsCollection tourObjectsCollection = new TourObjectsCollection();

            ObservableCollection<Tour> Tours = new ObservableCollection<Tour>(tourManager.GetTours());

            if (!Tours.Any())
            {
                MessageBox.Show("You have no Tours to export.", "No Tours found", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            StreamWriter jsonFile = new StreamWriter(exportJson);
            foreach (Tour t in Tours)
            {
                byte[] imageArray = File.ReadAllBytes(t.ImagePath);
                //string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                string base64ImageRepresentation = "img";
                TourObject tourObject = new TourObject(t, base64ImageRepresentation, new ObservableCollection<Log>(logManager.GetLogs(t.Id)));
                tourObjectsCollection.TourObjects.Add(tourObject);
            }
            string jsonString = JsonConvert.SerializeObject(tourObjectsCollection.TourObjects);
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
