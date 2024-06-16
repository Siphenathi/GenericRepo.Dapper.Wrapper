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

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			using var connection = GetConnection();
			var result = await connection.QueryAsync<TEntity>($"Select * from {_tableName}");
			return result;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(object id, string keyName)
		{
			using var connection = GetConnection();
			var result = await connection.QueryAsync<TEntity>($"Select * from {_tableName} where {keyName}=@Id", new { Id = id });
			return result;
		}

		public async Task<int> InsertOrUpdateAsync(object id, string keyName, TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			var entityWeLookFor = await GetAsync(id, keyName);
			return entityWeLookFor == null ? 
				await InsertAsync(entity, namesOfColumnsToBeExcluded) : 
				await UpdateAsync(keyName, entity, namesOfColumnsToBeExcluded);
		}

		public async Task<TEntity> GetAsync(object id, string keyName)
		{
			using var connection = GetConnection();
			var getRecordQuery = $"Select * from {_tableName} where {keyName}=@Id";
			var result = await connection.QuerySingleOrDefaultAsync<TEntity>(getRecordQuery, new { Id = id });
			return result;
		}

		public async Task<int> InsertAsync(TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			var entityPropertyProcessorResponse = EntityPropertyProcessor.FormatQueryStatementBody<TEntity>(QueryStatement.InsertQuery, namesOfColumnsToBeExcluded);
			if (entityPropertyProcessorResponse.Error != null)
				throw new Exception(entityPropertyProcessorResponse.Error.Message);

			var insertQuery = $"Insert into {_tableName} {entityPropertyProcessorResponse.Result}";
			using var connection = GetConnection();
			return await connection.ExecuteAsync(insertQuery, entity);
		}

		public async Task<int> UpdateAsync(string keyName, TEntity entity, params string[] namesOfColumnsToBeExcluded)
		{
			namesOfColumnsToBeExcluded = AddToList(namesOfColumnsToBeExcluded, keyName, false).ToArray();
			var entityPropertyProcessorResponse = EntityPropertyProcessor.FormatQueryStatementBody<TEntity>(QueryStatement.UpdateQuery, namesOfColumnsToBeExcluded);
			if (entityPropertyProcessorResponse.Error != null)
				throw new Exception(entityPropertyProcessorResponse.Error.Message);

			var updateQuery = $"update {_tableName} set {entityPropertyProcessorResponse.Result} where {keyName}=@{keyName}";
			using var connection = GetConnection();
			return await connection.ExecuteAsync(updateQuery, entity);
		}

		public async Task<int> DeleteAsync(object id, string keyName)
		{
			var deleteQuery = $"delete from {_tableName} where {keyName}=@Id";
			using var connection = GetConnection();
			return await connection.ExecuteAsync(deleteQuery, new { Id = id });
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

		private static IEnumerable<string> AddToList(IEnumerable<string> collection, string value, bool allowDuplicate)
		{
			var currentList = collection.ToList();
			if (!allowDuplicate && currentList.Contains(value)) return currentList;
			currentList.Add(value);
			return currentList;
		}
	}
}