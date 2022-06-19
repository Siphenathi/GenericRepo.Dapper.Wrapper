using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace GenericRepo.Dapper.Wrapper
{
	public interface IStoredProcProcessor
	{
		/// <summary>
		/// Get data from table using stored procedure with or without parameter
		/// </summary>
		/// <returns>
		/// Available table records
		/// </returns>
		Task<IEnumerable<T>> GetDataAsync<T>(string procName, DynamicParameters parameters = null);

		/// <summary>
		/// Execute data manipulation language (DML) stored procedures (insert, delete or update)
		/// </summary>
		/// <returns>
		/// Number of rows affected by calling the stored procedure with provided parameters
		/// </returns>
		Task<int> ExecuteAsync(string procName, DynamicParameters parameters);

		/// <summary>
		/// Execute in bulk data manipulation language (DML) stored procedures (insert, delete or update) with user-defined table type
		/// </summary>
		/// <returns>
		/// Number of rows affected by calling the stored procedure with provided parameters
		/// </returns>
		Task<int> ExecuteInBulkAsync(string procName, object @object );
	}
}