using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepo.Dapper.Wrapper
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(object id, string primaryKeyName);
		Task<IEnumerable<T>> GetAllAsync(object id, string primaryKeyName);
		Task<int> InsertAsync(T entity, params string[] namesOfPropertiesToBeExcluded);
		Task<int> UpdateAsync(string primaryKeyName, T entity, params string[] namesOfPropertiesToBeExcluded);
		Task<int> DeleteAsync(object id, string primaryKeyName);
	}
}
