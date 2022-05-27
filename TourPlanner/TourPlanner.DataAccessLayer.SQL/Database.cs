using System;
using System.Data;
using System.Data.Common;
using Npgsql;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.Configuration;

namespace TourPlanner.DataAccessLayer.SQL
{
    public class Database : IDatabase {
	    private readonly string _connectionString;

	    public Database() {
		    var config = ConfigManager.GetConfig(); 
		    _connectionString =
			    $"Host={config.DatabaseHost};Port={config.DatabasePort};Username={config.DatabaseUsername};Password={config.DatabasePassword};Database={config.DatabaseName}";
		}

	    public Database(string connectionString) {
		    _connectionString = connectionString; 
	    }

	    private DbConnection Connect() {
		    var connection = new NpgsqlConnection(_connectionString);
			connection.Open();
			return connection; 
	    }

	    public DbCommand CreateCommand(string cmdText) {
		    return new NpgsqlCommand(cmdText); 
	    }
		
	    /// <summary>
		/// Declares and initializes parameter with a value
		/// </summary>
		public void DefineParameter(DbCommand cmd, string name, DbType type, object value) {
		    cmd.Parameters[DeclareParameter(cmd, name, type)].Value = value; 
	    }

	    /// <summary>
		/// Creates new Command Parameter
		/// </summary>
		/// <param name="cmd">NpgSqlCommand to which parameter should be added</param>
		/// <param name="name">Parameter name</param>
		/// <param name="type">Parameter type</param>
		/// <returns>Index of newly created parameter</returns>
		/// <exception cref="ArgumentException">Throws Exception if parameter already exists</exception>
	    private int DeclareParameter(DbCommand cmd, string name, DbType type) {
		    if (!cmd.Parameters.Contains(name)) {
			    return cmd.Parameters.Add(new NpgsqlParameter(name, type)); 
		    }
		    throw new ArgumentException($"Parameter {name} already exists!"); 
	    }

		/// <summary>
		/// Sets value of a parameter
		/// </summary>
		/// <exception cref="ArgumentException">Throws exception if parameter does not exist</exception>
	    private void SetParameter(DbCommand cmd, string name, object value) {
		    if (cmd.Parameters.Contains(name)) cmd.Parameters[name].Value = value;
		    else throw new ArgumentException($"Parameter {name} does not exist");
	    }

		/// <summary>
		/// Execute Command with DataReader and close connection after executing
		/// </summary>
		/// <returns>DataReader</returns>
		public IDataReader ExecuteReader(DbCommand cmd)
		{
			cmd.Connection = Connect();
			cmd.Prepare();
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}

		/// <summary>
		/// Execute Command Scalar
		/// </summary>
		public int ExecuteScalar(DbCommand cmd)
		{
			cmd.Connection = Connect();
			cmd.Prepare();
			var value = Convert.ToInt32(cmd.ExecuteScalar());
			cmd.Connection.Close();
			return value;
		}

		public double ExecuteScalarToDouble(DbCommand cmd)
		{
			cmd.Connection = Connect();
			cmd.Prepare();
			var value = Convert.ToDouble(cmd.ExecuteScalar());
			cmd.Connection.Close();
			return value;
		}

		/// <summary>
		/// Execute Command NonQuery
		/// </summary>
		/// <returns>Number of rows affected by the command</returns>
		public int ExecuteNonQuery(DbCommand cmd)
		{
			cmd.Connection = Connect();
			cmd.Prepare();
			var value = cmd.ExecuteNonQuery();
			cmd.Connection.Close();
			return value;
		}
	}
}
