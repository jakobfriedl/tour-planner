using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.SQL
{
    public class LogDAO : ILogDAO {
	    private readonly IDatabase _db;

		private const string SqlGetLogByLogId = "SELECT * FROM \"log\" WHERE id=@id;"; 
	    private const string SqlGetLogsByTourId = "SELECT * FROM \"log\" WHERE tour_id=@tourId;"; 
	    private const string SqlInsertLog = "" +
	        "INSERT INTO \"log\"(tour_id, start_time, end_time, total_time, comment, difficulty, rating) " +
	        "VALUES (@tourId, @startTime, @endTime, @totalTime, @comment, @difficulty, @rating);" +
			"SELECT CAST(lastval() AS integer);";
	    private const string SqlDeleteLog = "DELETE FROM \"log\" WHERE @id=id;";
	    private const string SqlUpdateLog = "UPDATE \"log\" " +
             "SET start_time=@startTime, end_time=@endTime, total_time=@totalTime, comment=@comment, difficulty=@difficulty, rating=@rating " +
             "WHERE id=@id;";

		public LogDAO(IDatabase database) {
		    _db = database; 
	    }

		/// <summary>
		/// Get log by logId
		/// </summary>
		/// <param name="id">LogId</param>
		/// <returns>Log object</returns>
	    public Log GetLogByLogId(int id) {
		    var cmd = _db.CreateCommand(SqlGetLogByLogId);
			_db.DefineParameter(cmd, "@id", DbType.Int32, id);
		    return QueryLogs(cmd).FirstOrDefault()!; 
	    }

		/// <summary>
		/// Get logs from a specific Tour
		/// </summary>
		/// <param name="tourId">TourID</param>
		/// <returns>List of Logs</returns>
	    public IEnumerable<Log> GetLogsByTourId(int tourId) {
		    var cmd = _db.CreateCommand(SqlGetLogsByTourId);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, tourId);
		    return QueryLogs(cmd); 
	    }

	    public Log AddNewLog(Log log) {
		    var cmd = _db.CreateCommand(SqlInsertLog); 
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, log.TourId);
			_db.DefineParameter(cmd, "@startTime", DbType.DateTime2, log.StartTime);
			_db.DefineParameter(cmd, "@endTime", DbType.DateTime2, log.EndTime);
			_db.DefineParameter(cmd, "@totalTime", DbType.Int32, log.TotalTime);
			_db.DefineParameter(cmd, "@comment", DbType.String, log.Comment);
			_db.DefineParameter(cmd, "@difficulty", DbType.Int32, log.Difficulty);
			_db.DefineParameter(cmd, "@rating", DbType.Int32, log.Rating);
			return GetLogByLogId(_db.ExecuteScalar(cmd)); 
	    }

	    public Log UpdateLog(Log log) {
		    var cmd = _db.CreateCommand(SqlUpdateLog); 
			_db.DefineParameter(cmd, "@startTime", DbType.DateTime2,  log.StartTime);
			_db.DefineParameter(cmd, "@endTime", DbType.DateTime2, log.EndTime);
			_db.DefineParameter(cmd, "@totalTime", DbType.Int32, log.TotalTime);
			_db.DefineParameter(cmd, "@comment", DbType.String, log.Comment);
			_db.DefineParameter(cmd, "@difficulty", DbType.Int32, log.Difficulty);
			_db.DefineParameter(cmd, "@rating", DbType.Int32, log.Rating);
			_db.DefineParameter(cmd, "@id", DbType.Int32, log.Id);
			if (_db.ExecuteNonQuery(cmd) <= 0) {
				// Log LogUpdate error
			}
			return GetLogByLogId(log.Id); 
	    }

	    public bool DeleteLog(int id) {
		    var cmd = _db.CreateCommand(SqlDeleteLog); 
			_db.DefineParameter(cmd, "@id", DbType.Int32, id);
			return _db.ExecuteNonQuery(cmd) > 0; 
	    }

	    private IEnumerable<Log> QueryLogs(DbCommand cmd) {
		    var logs = new ObservableCollection<Log>();

		    using var reader = _db.ExecuteReader(cmd);
		    while (reader.Read())
		    {
			    logs.Add(new Log(
					(int)reader["id"],
					(int)reader["tour_id"],
					(DateTime)reader["start_time"],
					(DateTime)reader["end_time"],
					(int)reader["total_time"],
					(string)reader["comment"],
					(int)reader["difficulty"],
					(int)reader["rating"]
			    ));
		    }

		    return logs;
		}
    }
}
