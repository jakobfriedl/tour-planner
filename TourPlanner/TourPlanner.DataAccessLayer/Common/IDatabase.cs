using System.Data;
using System.Data.Common;

namespace TourPlanner.DataAccessLayer.Common {
	public interface IDatabase {
		DbCommand CreateCommand(string cmdText);
		int DeclareParameter(DbCommand cmd, string name, DbType type);
		void DefineParameter(DbCommand cmd, string name, DbType type, object value);
		void SetParameter(DbCommand cmd, string name, object value);

		IDataReader ExecuteReader(DbCommand cmd);
		int ExecuteScalar(DbCommand cmd);
		int ExecuteNonQuery(DbCommand cmd); 
	}
}