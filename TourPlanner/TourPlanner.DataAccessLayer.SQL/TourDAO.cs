using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.Configuration;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.SQL
{
    public class TourDAO : ITourDAO {
	    private readonly IDatabase _db;

		private const string SqlGetAllTours = "SELECT * FROM \"tour\" ORDER BY id asc;"; 
		private const string SqlGetTourById = "SELECT * FROM \"tour\" WHERE id=@id;";
		private const string SqlInsertTour = "" + 
		    "INSERT INTO \"tour\"(name, description, start, destination, transport_type, distance, time)" +
			"VALUES (@name, @description, @start, @destination, @transportType, @distance, @time);" +
			"SELECT CAST(lastval() AS integer);";
		private const string SqlSetTourImagePath = "UPDATE \"tour\" SET image_path=@imagePath WHERE id=@id;";
		private const string SqlDeleteTour = "DELETE FROM \"tour\" WHERE id=@id;"; 
		private const string SqlUpdateTour = "UPDATE \"tour\" " +
			"SET name=@name, description=@description, start=@start, destination=@destination, transport_type=@transportType, distance=@distance, time=@time " +
			"WHERE id=@id;";
		private const string SqlSearchTours = "SELECT DISTINCT tour.* FROM \"tour\" LEFT JOIN log ON (tour.id = log.tour_id) " +
		    "WHERE name LIKE @searchTerm OR description LIKE @searchTerm OR start LIKE @searchTerm OR destination LIKE @searchTerm " +
		    "OR log.comment LIKE @searchTerm;";

		public TourDAO(IDatabase database) {
		    _db = database; 
	    }

		/// <summary>
		/// Get Tour Object By TourID
		/// </summary>
		/// <param name="id">TourID</param>
		/// <returns>Tour</returns>
	    private Tour GetTourByTourId(int id) {
		    var cmd = _db.CreateCommand(SqlGetTourById); 
			_db.DefineParameter(cmd, "@id", DbType.Int32, id);
			return QueryTours(cmd).FirstOrDefault()!; 
	    }

		/// <summary>
		/// Create a new Tour in the DB
		/// </summary>
		/// <param name="tour">Tour to create</param>
		/// <returns>Created Tour</returns>
	    public Tour AddNewTour(Tour tour) {
		    var cmd = _db.CreateCommand(SqlInsertTour);
			_db.DefineParameter(cmd, "@name", DbType.String, tour.Name);
			_db.DefineParameter(cmd, "@description", DbType.String, tour.Description);
			_db.DefineParameter(cmd, "@start", DbType.String, tour.Start);
			_db.DefineParameter(cmd, "@destination", DbType.String, tour.Destination);
			_db.DefineParameter(cmd, "@transportType", DbType.Int32, (int)tour.TransportType);
			_db.DefineParameter(cmd, "@distance", DbType.Double, tour.Distance);
			_db.DefineParameter(cmd, "@time", DbType.Int32, tour.EstimatedTime);
			return GetTourByTourId(_db.ExecuteScalar(cmd));
		}

		/// <summary>
		/// Delete Tour with specific ID
		/// </summary>
		/// <param name="id">Tour ID</param>
		/// <returns>True if tour successfully deleted</returns>
		public bool DeleteTour(int id) {
			var cmd = _db.CreateCommand(SqlDeleteTour); 
			_db.DefineParameter(cmd, "@id", DbType.Int32, id);
			return _db.ExecuteNonQuery(cmd) > 0; 
		}

		/// <summary>
		/// Update the value of imagePath of a tour, since the image is named after the id,
		/// which is not known when creating the tour
		/// </summary>
		/// <param name="id">ID of the tour</param>
		/// <param name="imagePath">Relative Image path: Resources/tour-img/...</param>
		/// <returns>Number of Rows changed</returns>
	    public int SetImagePath(int id, string imagePath) {
		    var cmd = _db.CreateCommand(SqlSetTourImagePath);
			_db.DefineParameter(cmd, "@imagePath", DbType.String, imagePath);
			_db.DefineParameter(cmd, "@id", DbType.Int32, id);
			return _db.ExecuteNonQuery(cmd); 
	    }

		/// <summary>
		/// Save Updated tour object in database 
		/// </summary>
		/// <param name="tour">Updated object</param>
		/// <returns>Updated Tour</returns>
		public Tour UpdateTour(Tour tour) {
			var cmd = _db.CreateCommand(SqlUpdateTour);
			_db.DefineParameter(cmd, "@name", DbType.String, tour.Name);
			_db.DefineParameter(cmd, "@description", DbType.String, tour.Description);
			_db.DefineParameter(cmd, "@start", DbType.String, tour.Start);
			_db.DefineParameter(cmd, "@destination", DbType.String, tour.Destination);
			_db.DefineParameter(cmd, "@transportType", DbType.Int32, (int)tour.TransportType);
			_db.DefineParameter(cmd, "@distance", DbType.Double, tour.Distance);
			_db.DefineParameter(cmd, "@time", DbType.Int32, tour.EstimatedTime);
			_db.DefineParameter(cmd, "@id", DbType.Int32, tour.Id);
			if (_db.ExecuteNonQuery(cmd) <= 0) {
				// Log TourUpdate error; 
			}
			return GetTourByTourId(tour.Id);
		}

		/// <summary>
		/// Get All Tours
		/// </summary>
		/// <returns>All Tours in a enumerable format</returns>
	    public IEnumerable<Tour> GetTours() {
			var cmd = _db.CreateCommand(SqlGetAllTours);
		    return QueryTours(cmd);
	    }

		/// <summary>
		/// Search Tours for specific string in name, description, start or destination
		/// </summary>
		/// <param name="searchTerm">Search term, later put between %-wildcards</param>
		/// <returns>List of matching tours, returns all tours if nothing is searched</returns>
		public IEnumerable<Tour> SearchTours(string searchTerm) {
			var cmd = _db.CreateCommand(SqlSearchTours);
			_db.DefineParameter(cmd, "@searchTerm", DbType.String, $"%{searchTerm}%");
			return string.IsNullOrEmpty(searchTerm) ? GetTours() : QueryTours(cmd); 
		}

		/// <summary>
		/// Query the database for Tours
		/// </summary>
		private IEnumerable<Tour> QueryTours(DbCommand cmd) {
		    var tours = new ObservableCollection<Tour>();

		    using var reader = _db.ExecuteReader(cmd);
		    while (reader.Read()) {
			    tours.Add(new Tour(
				    (int)reader["id"],
				    (string)reader["name"],
				    (string)reader["description"],
				    (string)reader["start"],
				    (string)reader["destination"],
				    (TransportType)reader["transport_type"],
				    (double)reader["distance"],
				    (int)reader["time"],
				    (string)reader["image_path"],
				    (int)reader["popularity"],
				    (int)reader["child_friendliness"]
			    ));
		    }
		    
		    return tours;
	    }
    }
}
