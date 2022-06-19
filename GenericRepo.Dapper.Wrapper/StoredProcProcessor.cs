using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace GenericRepo.Dapper.Wrapper
{
	public class StoredProcProcessor
	{
		private readonly string _connectionString;
		public StoredProcProcessor(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <summary>
		/// Get data from table using stored procedure with or without parameter
		/// </summary>
		/// <returns>
		/// Available table records
		/// </returns>
		public async Task<IEnumerable<T>> GetDataAsync<T>(string procName, DynamicParameters parameters = null)
		{
			using IDbConnection connection = GetConnection();
			return await connection.QueryAsync<T>(procName, parameters, commandType: CommandType.StoredProcedure);
		}

		/// <summary>
		/// Execute data manipulation language (DML) stored procedures (insert, delete or update)
		/// </summary>
		/// <returns>
		/// Number of rows affected by calling the stored procedure with provided parameters
		/// </returns>
		public async Task<int> ExecuteAsync(string procName, DynamicParameters parameters)
		{
			using IDbConnection connection = GetConnection();
			return await connection.ExecuteAsync(procName, parameters, commandType: CommandType.StoredProcedure);
		}

		/// <summary>
		/// Execute in bulk data manipulation language (DML) stored procedures (insert, delete or update) with user-defined table type
		/// </summary>
		/// <returns>
		/// Number of rows affected by calling the stored procedure with provided parameters
		/// </returns>
		public async Task<int> ExecuteInBulkAsync(string procName, object @object )
		{
			using IDbConnection connection = GetConnection();
			return await connection.ExecuteAsync(procName, @object, commandType: CommandType.StoredProcedure);
		}

		private SqlConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
