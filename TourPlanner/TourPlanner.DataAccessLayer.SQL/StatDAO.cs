﻿using System;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Logging;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;

namespace TourPlanner.DataAccessLayer.SQL {
	public class StatDAO : IStatDAO {
		private readonly ILogger _logger; 
		private readonly IDatabase _db;

		private const string SqlGetLogCount = "SELECT COUNT(*) FROM \"log\" WHERE tour_id=@tourId;";
		private const string SqlGetAvgRating = "SELECT CAST(AVG(rating) AS double precision) FROM \"log\" WHERE tour_id=@tourId;";
		private const string SqlGetAvgDifficulty = "SELECT CAST(AVG(difficulty) AS double precision) FROM \"log\" WHERE tour_id=@tourId;";
		private const string SqlGetAvgDuration = "SELECT CAST(AVG(total_time) AS integer) FROM \"log\" WHERE tour_id=@tourId;";
		private const string SqlGetDuration = "SELECT time FROM \"tour\" WHERE id=@tourId;";

		public StatDAO(IDatabase database, ILogger logger) {
			_db = database;
			_logger = logger; 
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
			} catch (InvalidCastException e) {
				_logger.LogWarning($"Invalid Cast Exception. No Logs for tour [id: {id}] have been found. Average Rating is set to 0. {DateTime.UtcNow}");
				return 0;
			}
		}

		public double GetAvgDifficulty(int id) {
			var cmd = _db.CreateCommand(SqlGetAvgDifficulty);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, id);
			try {
				return Math.Round(_db.ExecuteScalarToDouble(cmd), 2);
			} catch (InvalidCastException) {
				_logger.LogWarning($"Invalid Cast Exception. No Logs for tour [id: {id}] have been found. Average Difficulty is set to 0. {DateTime.UtcNow}");
				return 0;
			}
		}

		public int GetAvgDuration(int id) {
			var cmd = _db.CreateCommand(SqlGetAvgDuration);
			var cmd2 = _db.CreateCommand(SqlGetDuration);
			_db.DefineParameter(cmd, "@tourId", DbType.Int32, id);
			_db.DefineParameter(cmd2, "@tourId", DbType.Int32, id);
			try {
				return _db.ExecuteScalar(cmd);
			} catch (InvalidCastException) {
				_logger.LogWarning($"Invalid Cast Exception. No Logs for tour [id: {id}] have been found. Average Difficulty is set to 0. {DateTime.UtcNow}");
				return _db.ExecuteScalar(cmd2); 
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
