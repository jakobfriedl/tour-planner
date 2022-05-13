using System;
using System.Data;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;

namespace TourPlanner.DataAccessLayer.SQL {
	public class StatDAO : IStatDAO {
		private readonly IDatabase _db;

		private const string SqlGetAvgRating = "SELECT CAST(AVG(rating) AS double precision) FROM \"log\" WHERE tour_id=@tourId;";
		private const string SqlGetAvgDifficulty = "SELECT CAST(AVG(difficulty) AS double precision) FROM \"log\" WHERE tour_id=@tourId;";
		private const string SqlGetLogCount = "SELECT COUNT(*) FROM \"log\" WHERE tour_id=@tourId;";

		public StatDAO(IDatabase database) {
			_db = database;
		}
		
		public int GetLogCount(int id) {
			var cmd = _db.CreateCommand(SqlGetLogCount);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, id);
			return _db.ExecuteScalar(cmd);
		}

		public double GetAvgRating(int id) {
			var cmd = _db.CreateCommand(SqlGetAvgRating);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, id);
			try {
				return Math.Round(_db.ExecuteScalarToDouble(cmd), 2);
			} catch (InvalidCastException) {
				return 0;
			}
		}

		public double GetAvgDifficulty(int id) {
			var cmd = _db.CreateCommand(SqlGetAvgDifficulty);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, id);
			try {
				return Math.Round(_db.ExecuteScalarToDouble(cmd), 2);
			} catch (InvalidCastException) {
				return 0;
			}
		}

		/// <summary>
		/// Calculate Popularity: Count * Avg-Rating 
		/// </summary>
		/// <param name="id">Tour Id</param>
		/// <returns>Popularity to 2 decimal digits</returns>
		public double GetPopularity(int id) {
			return GetLogCount(id) * GetAvgRating(id); 
		}

		/// <summary>
		/// Calculate Child-Friendliness: 10 - Avg-Difficulty
		/// </summary>
		/// <param name="id">Tour Id</param>
		/// <returns>0 if there are no logs, otherwise child-friendliness to 2 decimal digits</returns>
		public double GetChildFriendliness(int id) {
			return GetLogCount(id) <= 0 ? 0 : 10 - GetAvgDifficulty(id);
		}
	}
}
