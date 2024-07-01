using Dapper;
using GenericRepo.Dapper.Wrapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using GenericRepo.Dapper.Wrapper.Interface;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

namespace GenericRepo.Dapper.Wrapper
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly string _tableName;
		private readonly string _connectionString;
		private readonly DatabaseProvider _databaseProvider;
		public Repository(string tableName, string connectionString, DatabaseProvider databaseProvider)
		{
			_tableName = tableName;
			_connectionString = connectionString;
			_databaseProvider = databaseProvider;
		}

		async Task<IEnumerable<TEntity>> IRepository<TEntity>.GetAllAsync()
		{
			using var connection = GetConnection();
			var result = await connection.QueryAsync<TEntity>($"Select * from {_tableName}");
			return result;
		}

		async Task<IEnumerable<TEntity>> IRepository<TEntity>.GetAllAsync(Dictionary<string, object> parameters)
		{
			using var connection = GetConnection();
			var sqlQuery = $"Select * from {_tableName} where {EntityPropertyProcessor.GetFormatWhereClauseOfQueryStatement(parameters.Keys)}";
			return await connection.QueryAsync<TEntity>(sqlQuery, new DynamicParameters(parameters));
		}

		async Task<TEntity> IRepository<TEntity>.GetAsync(Dictionary<string, object> parameters)
		{
			return await GetAsync(parameters);
		}

		async Task<int> IRepository<TEntity>.InsertOrUpdateAsync(Dictionary<string, object> parameters, TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			var entityWeLookFor = await GetAsync(parameters);
			return entityWeLookFor == null ?
				await InsertAsync(entity, namesOfColumnsToBeExcluded) :
				await UpdateAsync(parameters, entity, namesOfColumnsToBeExcluded);
		}

		async Task<int> IRepository<TEntity>.InsertAsync(TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			return await InsertAsync(entity, namesOfColumnsToBeExcluded);
		}

		async Task<int> IRepository<TEntity>.UpdateAsync(Dictionary<string, object> parameters, TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			return await UpdateAsync(parameters, entity, namesOfColumnsToBeExcluded);
		}

		async Task<int> IRepository<TEntity>.DeleteAsync(Dictionary<string, object> parameters)
		{
			if (parameters == null || !parameters.Any())
				return 0;

			using var connection = GetConnection();
			var deleteQuery = $"delete from {_tableName} where {EntityPropertyProcessor.GetFormatWhereClauseOfQueryStatement(parameters.Keys)}";
			return await connection.ExecuteAsync(deleteQuery, new DynamicParameters(parameters));
		}

		private async Task<TEntity> GetAsync(Dictionary<string, object> parameters)
		{
			if (parameters == null || !parameters.Any())
				return default(TEntity);
			using var connection = GetConnection();
			var getRecordQuery = $"Select * from {_tableName} where " +
			                     $"{EntityPropertyProcessor.GetFormatWhereClauseOfQueryStatement(parameters.Keys)}";
			return await connection.QuerySingleOrDefaultAsync<TEntity>(getRecordQuery, new DynamicParameters(parameters));
		}

		private async Task<int> InsertAsync(TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			var entityPropertyProcessorResponse = EntityPropertyProcessor.GetFormatQueryStatementBody<TEntity>(QueryStatement.InsertQuery, namesOfColumnsToBeExcluded);
			if (entityPropertyProcessorResponse.Error != null)
				throw new Exception(entityPropertyProcessorResponse.Error.Message);

			var insertQuery = $"insert into {_tableName} {entityPropertyProcessorResponse.Result}";
			using var connection = GetConnection();
			return await connection.ExecuteAsync(insertQuery, entity);
		}

		private async Task<int> UpdateAsync(Dictionary<string, object> parameters, TEntity entity,
			params string[] namesOfColumnsToBeExcluded)
		{
			var entityPropertyProcessorResponse = EntityPropertyProcessor.GetFormatQueryStatementBody<TEntity>(QueryStatement.UpdateQuery, namesOfColumnsToBeExcluded);
			if (entityPropertyProcessorResponse.Error != null)
				throw new Exception(entityPropertyProcessorResponse.Error.Message);

			using var connection = GetConnection();
			var updateQuery = $"update {_tableName} set {entityPropertyProcessorResponse.Result} where " +
			                  $"{EntityPropertyProcessor.GetFormatWhereClauseOfQueryStatement(parameters.Keys)}";
			return await connection.ExecuteAsync(updateQuery, entity);
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