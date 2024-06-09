using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading.Tasks;
using Dapper;
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

namespace GenericRepo.Dapper.Wrapper
{
	public class StoredProcProcessor : IStoredProcProcessor
	{
		private readonly string _connectionString;
		private readonly DatabaseProvider _databaseProvider;
		private readonly DatabaseProvider obj = DatabaseProvider.MySql;
		public StoredProcProcessor(string connectionString, DatabaseProvider databaseProvider)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentNullException(nameof(connectionString));
			_connectionString = connectionString;
			_databaseProvider = databaseProvider;
		}

		/// <summary>
		/// Get data from table using stored procedure with or without parameter
		/// </summary>
		/// <returns>
		/// table rows
		/// </returns>
		async Task<IEnumerable<T>> IStoredProcProcessor.GetDataAsync<T>(string procName, object parameters)
		{
			using var connection = GetConnection();
			return await connection.QueryAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure);
		}

		/// <summary>
		/// Execute data manipulation language (DML) stored procedures (insert, delete or update)
		/// </summary>
		/// <returns>
		/// Number of rows affected
		/// </returns>
		async Task<int> IStoredProcProcessor.ExecuteAsync(string procName, object parameters)
		{
			using var connection = GetConnection();
			return await connection.ExecuteAsync(procName, parameters, commandType: CommandType.StoredProcedure);
		}

		/// <summary>
		/// Execute in bulk data manipulation language (DML) stored procedures (insert, delete or update) with user-defined table type
		/// </summary>
		/// <returns>
		/// Number of rows affected
		/// </returns>
		async Task<int> IStoredProcProcessor.ExecuteInBulkAsync(string procName, object @object )
		{
			using var connection = GetConnection();
			return await connection.ExecuteAsync(procName, @object, commandType: CommandType.StoredProcedure);
		}

		private IDbConnection GetConnection()
		{
			return _databaseProvider switch
			{
				DatabaseProvider.MySql => new MySqlConnection(_connectionString),
				DatabaseProvider.Oracle => new OracleConnection(_connectionString),
				DatabaseProvider.SqLite => new SQLiteConnection(_connectionString),
				DatabaseProvider.PostGreSql => new NpgsqlConnection(_connectionString),
				_ => new SqlConnection(_connectionString)
			};
		}
	}
}
