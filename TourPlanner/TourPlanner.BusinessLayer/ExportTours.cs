using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.Models;
using TourPlanner.Models.Json;

namespace TourPlanner.BusinessLayer {
	public class ExportTours {
		private readonly ILogger _logger;

		public ExportTours(ILogger logger) {
			_logger = logger;
		}

		public void Export() {
			var location = Path.Combine(Directory.GetCurrentDirectory(), ConfigManager.GetConfig().ExportLocation);

			var tourManager = new TourManager(_logger);
			var logManager = new LogManager(_logger);
			var tourObjectCollection = new TourObjectCollection();

			var tours = new List<Tour>(tourManager.GetTours());

			if (!tours.Any()) {
				_logger.LogWarning($"No Tours to export. {DateTime.UtcNow}");
				MessageBox.Show("You have no Tours to export.", "No Tours found", MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			if (!Directory.Exists(location)) {
				_logger.LogWarning($"Directory does not exist: {location}. {DateTime.UtcNow}");
				Directory.CreateDirectory(location);
			}

			var openFileDialog = new SaveFileDialog {
				Filter = "json file(*.json)| *.json",
				Title = "Save the Export as JSON"
			};
			openFileDialog.ShowDialog();
			var exportJson = openFileDialog.FileName;

			var jsonFile = new StreamWriter(exportJson);
			foreach (var t in tours) {
				var imageArray = File.ReadAllBytes(t.ImagePath);
				var base64ImageRepresentation = Convert.ToBase64String(imageArray);
				var tourObject = new TourObject(t, base64ImageRepresentation, new List<Log>(logManager.GetLogs(t.Id)));
				tourObjectCollection.TourObjects.Add(tourObject);
			}

			var jsonString = JsonConvert.SerializeObject(tourObjectCollection, Formatting.Indented);
			jsonFile.WriteLine(jsonString);
			jsonFile.Flush();

			if (Directory.Exists(location)) {
				var startInfo = new ProcessStartInfo("explorer.exe", location);
				Process.Start(startInfo);
			} else {
				MessageBox.Show($"Exporting the Tours was not successful.", "Tours Export Error", MessageBoxButton.OK,
					MessageBoxImage.Error);
			}
		}
	}
}