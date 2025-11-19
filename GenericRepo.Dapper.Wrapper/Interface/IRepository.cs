using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepo.Dapper.Wrapper.Interface
{
	public interface IRepository<T>
	{
		/// <summary>
		/// Gets all records from the table
		/// </summary>
		/// <returns>
		/// List of provided entity records
		/// </returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Gets all matching records from the table
		/// </summary>
		/// <param name="parameters">List of column names with their corresponding values to be used to filter for records</param>
		/// <returns>
		/// List of provided entity records
		/// </returns>
		Task<IEnumerable<T>> GetAllAsync(Dictionary<string, object> parameters);

		/// <summary>
		/// Gets one matching record from the table
		/// </summary>
		/// <param name="parameters">List of column names with their corresponding values to be used to filter for a record</param>
		/// <returns>
		/// One matching entity record
		/// </returns>
		Task<T> GetAsync(Dictionary<string, object> parameters);

		/// <summary>
		/// Insert one record 
		/// </summary>
		/// <param name="entity">Name of the table to create a record to</param>
		/// <param name="namesOfColumnsToBeExcluded">Names of columns to be excluded when creating a record ie primary key and other columns that are autopopulated by the database </param>
		/// <returns>
		/// number of records affected by the query
		/// </returns>
		Task<int> InsertAsync(T entity, params string[] namesOfColumnsToBeExcluded);

		/// <summary>
		/// updates record 
		/// </summary>
		/// <param name="parameters">List of column names with their corresponding values to be used to filter for a record</param>
		/// <param name="entity">Name of the table to create or update a record to</param>
		/// <param name="namesOfColumnsToBeExcluded">Names of columns to be excluded when creating a record ie primary key and other columns that are autopopulated by the database </param>
		/// <returns>
		/// number of records affected by the query
		/// </returns>
		Task<int> UpdateAsync(Dictionary<string, object> parameters, T entity, params string[] namesOfColumnsToBeExcluded);

		/// <summary>
		/// updates if the record exist or insert if not exist 
		/// </summary>
		/// <param name="parameters">List of column names with their corresponding values to be used to filter for a record</param>
		/// <param name="entity">Name of the table to create or update a record to</param>
		/// <param name="namesOfColumnsToBeExcluded">Names of columns to be excluded when creating a record ie primary key and other columns that are autopopulated by the database </param>
		/// <returns>
		/// number of records affected by the query
		/// </returns>
		Task<int> InsertOrUpdateAsync(Dictionary<string, object> parameters, T entity, params string[] namesOfColumnsToBeExcluded);

		/// <summary>
		/// Deletes a record
		/// </summary>
		/// <param name="parameters">List of column names with their corresponding values to be used to filter for a record</param>
		/// <returns>
		/// number of records affected by the query
		/// </returns>
		Task<int> DeleteAsync(Dictionary<string, object> parameters);
	}
}