﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using TourPlanner.BusinessLayer.Abstract;
using TourPlanner.BusinessLayer.Exceptions;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.DataAccessLayer.REST;
using TourPlanner.DataAccessLayer.SQL;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourManager : ITourManager {
	    private readonly ITourDAO _tourDao;

	    public TourManager() {
		    _tourDao = new TourDAO(new Database()); 
	    }

	    public TourManager(Database db) {
		    _tourDao = new TourDAO(db);
	    }

		/// <summary>
		/// Create a tour by getting the needed information from the http-client,
		/// creating a db-entry and afterwards saving the image on the filesystem and
		/// the image-path in the db
		/// </summary>
		/// <param name="tour">Has name, description, start, dest., and transport type</param>
		/// <returns></returns>
		public async Task<Tour> CreateTour(Tour tour) {
		    return await SaveImage(await SaveInformation(tour));
	    }

		public async Task<Tour> UpdateTour(Tour tour) {
			return await SaveImage(await UpdateInformation(tour)); 
		}

		public bool DeleteTour(int id) {
			return _tourDao.DeleteTour(id); 
		}

	    public IEnumerable<Tour> GetTours() {
		    return _tourDao.GetTours();
	    }

	    public IEnumerable<Tour> SearchTours(string searchTerm) {
		    return _tourDao.SearchTours(searchTerm); 
	    }

		/// <summary>
		/// Get Initial Information for Tour from API and save this information to database
		/// </summary>
		/// <param name="tour">Tour to create</param>
		/// <returns>Tour with distance and time and id</returns>
		/// <exception cref="InvalidLocationException">Invalid Locations that could not be found, or the same location twice</exception>
	    private async Task<Tour> GetInformation(Tour tour) {
		    var http = new HttpRequest(new HttpClient());

		    try {
			    tour = await http.GetTourInformation(tour);
		    } catch (NullReferenceException) { throw new InvalidLocationException(); }

		    // Check for Invalid Locations
			if(tour.Distance == 0 || tour.EstimatedTime == 0) throw new InvalidLocationException();

			return tour; 
		}

		private async Task<Tour> SaveInformation(Tour tour) {
			return _tourDao.AddNewTour(await GetInformation(tour));
		}

		private async Task<Tour> UpdateInformation(Tour tour) {
			return _tourDao.UpdateTour(await GetInformation(tour)); 
		}

		/// <summary>
		/// Get Tour Image from API and save image-path in database
		/// </summary>
		/// <param name="tour">Tour to get image from</param>
		/// <returns>Tour with ImagePath</returns>
		private async Task<Tour> SaveImage(Tour tour) {
		    var http = new HttpRequest(new HttpClient());

		    // Save image from REST Request to png-File
		    var imageBytes = await http.GetTourImageBytes(tour);
		    tour.ImagePath = Path.Combine(ConfigManager.GetConfig().ImageLocation!, $"{tour.Id}.png");

		    await File.WriteAllBytesAsync(tour.ImagePath, imageBytes);

		    _tourDao.SetImagePath(tour.Id, tour.ImagePath);

		    return tour; 
	    }
    }
}
