using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using GenericRepo.Dapper.Wrapper.Interface;

namespace GenericRepo.Dapper.Wrapper
{
	public class StoredProcProcessor : IStoredProcProcessor
	{
		private readonly string _connectionString;
		public StoredProcProcessor(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentNullException(nameof(connectionString));
			_connectionString = connectionString;
		}

		/// <summary>
		/// Get data from table using stored procedure with or without parameter
		/// </summary>
		/// <returns>
		/// table rows
		/// </returns>
		async Task<IEnumerable<T>> IStoredProcProcessor.GetDataAsync<T>(string procName, object parameters)
		{
			using IDbConnection connection = GetConnection();
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
			using IDbConnection connection = GetConnection();
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
			using IDbConnection connection = GetConnection();
			return await connection.ExecuteAsync(procName, @object, commandType: CommandType.StoredProcedure);
		}

		private SqlConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
