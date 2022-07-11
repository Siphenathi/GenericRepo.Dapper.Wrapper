using Dapper;
using GenericRepo.Dapper.Wrapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GenericRepo.Dapper.Wrapper.Interface;

namespace GenericRepo.Dapper.Wrapper
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly string _tableName;
		private readonly string _connectionString;
		public Repository(string tableName, string connectionString)
		{
			_tableName = tableName;
			_connectionString = connectionString;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			using IDbConnection connection = GetConnection();
			var result = await connection.QueryAsync<TEntity>($"Select * from {_tableName}");
			return result;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(object id, string primaryKeyName)
		{
			using IDbConnection connection = GetConnection();
			var result = await connection.QueryAsync<TEntity>($"Select * from {_tableName} where {primaryKeyName}=@Id", new { Id = id });
			return result;
		}

		public async Task<TEntity> GetAsync(object id, string primaryKeyName)
		{
			using IDbConnection connection = GetConnection();
			var getRecordQuery = $"Select * from {_tableName} where {primaryKeyName}=@Id";
			var result = await connection.QuerySingleOrDefaultAsync<TEntity>(getRecordQuery, new { Id = id });

			if (result == null)
				throw new KeyNotFoundException($"{_tableName[..^1]} with {primaryKeyName} [{id}] could not be found.");
			return result;
		}

		public async Task<int> InsertAsync(TEntity entity, params string[] namesOfPropertiesToBeExcluded)
		{
			var entityPropertyProcessorResponse = EntityPropertyProcessor.FormatQueryStatementBody<TEntity>(QueryStatement.InsertQuery, namesOfPropertiesToBeExcluded);
			if (entityPropertyProcessorResponse.Error != null)
				throw new Exception(entityPropertyProcessorResponse.Error.Message);

			var insertQuery = $"Insert into {_tableName} {entityPropertyProcessorResponse.Result}";
			using IDbConnection connection = GetConnection();
			return await connection.ExecuteAsync(insertQuery, entity);
		}

		public async Task<int> UpdateAsync(string primaryKeyName, TEntity entity, params string[] namesOfPropertiesToBeExcluded)
		{
			namesOfPropertiesToBeExcluded = AddToList(namesOfPropertiesToBeExcluded, primaryKeyName, false).ToArray();
			var entityPropertyProcessorResponse = EntityPropertyProcessor.FormatQueryStatementBody<TEntity>(QueryStatement.UpdateQuery, namesOfPropertiesToBeExcluded);
			if (entityPropertyProcessorResponse.Error != null)
				throw new Exception(entityPropertyProcessorResponse.Error.Message);

			var updateQuery = $"update {_tableName} set {entityPropertyProcessorResponse.Result} where {primaryKeyName}=@{primaryKeyName}";
			using IDbConnection connection = GetConnection();
			return await connection.ExecuteAsync(updateQuery, entity);
		}

		public async Task<int> DeleteAsync(object id, string primaryKeyName)
		{
			var deleteQuery = $"delete from {_tableName} where {primaryKeyName}=@Id";
			using IDbConnection connection = GetConnection();
			return await connection.ExecuteAsync(deleteQuery, new { Id = id });
		}

		private SqlConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
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
