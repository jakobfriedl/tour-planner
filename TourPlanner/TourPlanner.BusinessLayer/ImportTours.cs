using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models.Json;

namespace TourPlanner.BusinessLayer {
	public class ImportTours {
		private readonly ILogger _logger;

		public ImportTours(ILogger logger) {
			_logger = logger;
		}

		public string ChooseImportFile() {
			var openFileDialog = new OpenFileDialog {
				InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), ConfigManager.GetConfig().ExportLocation),
				Filter = "json files (*.json)|*.json"
			};
			openFileDialog.ShowDialog(); 
			return openFileDialog.FileName;
		}

		public void Import() {
			var file = ChooseImportFile();
			if (string.IsNullOrEmpty(file)) {
				_logger.LogWarning($"No valid import file chosen. {DateTime.UtcNow}");
				MessageBox.Show($"No import file chosen.", "No Import File", MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			var tourDao = new TourDAO(new Database(), _logger);
			var logDao = new LogDAO(new Database(), _logger);

			var importFile = File.ReadAllText(file);
			var tourObjectList = JsonConvert.DeserializeObject<TourObjectCollection>(importFile);

			foreach (var tourObject in tourObjectList.TourObjects) {
				var newTour = tourDao.AddNewTour(tourObject.Tour);
				newTour.ImagePath = Path.Combine(ConfigManager.GetConfig().ImageLocation!, $"{newTour.Id}.png");
				tourDao.SetImagePath(newTour.Id, newTour.ImagePath);

				var imageBytes = Convert.FromBase64String(tourObject.ImageInBase64);
				File.WriteAllBytesAsync(newTour.ImagePath, imageBytes);
				foreach (var log in tourObject.Logs) {
					log.TourId = newTour.Id;
					logDao.AddNewLog(log);
				}
			}
		}
	}
}