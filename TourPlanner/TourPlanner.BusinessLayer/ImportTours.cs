using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using TourPlanner.Models;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using TourPlanner.Models.JSON;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.DataAccessLayer.Configuration;

namespace TourPlanner.BusinessLayer
{
    public class ImportTours
    {
        private string importFilePath { get; set; }

        public void chooseImportFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = $"{Directory.GetCurrentDirectory()}\\Resources\\exports";
            openFileDialog.Filter = "json files (*.json)|*.json";
            openFileDialog.ShowDialog();
            importFilePath = openFileDialog.FileName;
        }

        public void Import()
        {
            chooseImportFile();
            if(importFilePath == null)
            {
                MessageBox.Show($"No Import Filen chosen.", "No Import File", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TourDAO tourDAO = new TourDAO(new Database());
            LogDAO logDAO = new LogDAO(new Database());

            var importFile = File.ReadAllText(importFilePath);
            TourObjectsCollection tourObjectsList = JsonConvert.DeserializeObject<TourObjectsCollection>(importFile);
            foreach (var tourObject in tourObjectsList.TourObjects)
            {
                Tour newTour = tourDAO.AddNewTour(tourObject.Tour);
                newTour.ImagePath = Path.Combine(ConfigManager.GetConfig().ImageLocation!, $"{newTour.Id}.png");
                tourDAO.SetImagePath(newTour.Id, newTour.ImagePath);

                byte[] imageBytes = Convert.FromBase64String(tourObject.ImageInBase64);
                File.WriteAllBytesAsync(newTour.ImagePath, imageBytes);
                foreach (var log in tourObject.Logs)
                {
                    log.TourId = newTour.Id;
                    logDAO.AddNewLog(log);
                }
            }
        }
    }
}
