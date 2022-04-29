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
	        "INSERT INTO \"log\"(tour_id, date_time, total_time, comment, difficulty, rating) " +
	        "VALUES (@tourId, @dateTime, @totalTime, @comment, @difficulty, @rating);" +
			"SELECT CAST(lastval() AS integer);";  

	    public LogDAO(IDatabase database) {
		    _db = database; 
	    }

	    public Log GetLogByLogId(int id) {
		    var cmd = _db.CreateCommand(SqlGetLogByLogId);
			_db.DefineParameter(cmd, "@id", DbType.Int32, id);
		    return QueryLogs(cmd).FirstOrDefault()!; 
	    }

	    public IEnumerable<Log> GetLogsByTourId(int tourId) {
		    var cmd = _db.CreateCommand(SqlGetLogsByTourId);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, tourId);
		    return QueryLogs(cmd); 
	    }

	    public Log AddNewLog(Log log) {
		    var cmd = _db.CreateCommand(SqlInsertLog); 
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, log.TourId);
			_db.DefineParameter(cmd, "@dateTime", DbType.DateTime2, log.DateTime);
			_db.DefineParameter(cmd, "@totalTime", DbType.Int32, log.TotalTime);
			_db.DefineParameter(cmd, "@comment", DbType.String, log.Comment);
			_db.DefineParameter(cmd, "@difficulty", DbType.Int32, log.Difficulty);
			_db.DefineParameter(cmd, "@rating", DbType.Int32, log.Rating);
			return GetLogByLogId(_db.ExecuteScalar(cmd)); 
	    }

	    public Log UpdateLog(Log log) {
		    throw new NotImplementedException();
	    }

	    public bool DeleteLog(int id) {
		    throw new NotImplementedException();
	    }

	    private IEnumerable<Log> QueryLogs(DbCommand cmd) {
		    var logs = new ObservableCollection<Log>();

		    using var reader = _db.ExecuteReader(cmd);
		    while (reader.Read())
		    {
			    logs.Add(new Log(
					(int)reader["id"],
					(int)reader["tour_id"],
					(DateTime)reader["date_time"],
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
