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
            }
            TourObjectsCollection tourObjectsCollection = new TourObjectsCollection();
            //var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64String)));

            var importFile = File.ReadAllText(importFilePath);
            //List<TourObject> tourObjectsList = JsonConvert.DeserializeObject<List<TourObject>>(importFile);
            //var tours = JsonConvert.DeserializeObject<List<TourObject>>(importFile);


            var obj = JsonConvert.DeserializeObject<dynamic>(importFile);
            for (int i = 0; i < obj.Count; i++)
            {
                byte[] data = null;
                var item = obj[i].Tour;
                Tour tour = new Tour((int)item.Id, (string)item.Name, (string)item.Description, (string)item.Start, (string)item.Destination, (TransportType)item.TransportType, (double)item.Distance, (int)item.EstimatedTime, (string)item.ImagePath, (double)item.Popularity, (double)item.ChildFriendliness, (string)item.DisplayDistance, (string)item.DisplayTime, data);

                byte[] img = Convert.FromBase64String(obj[i].Image);
                
            }
        }
    }
}
