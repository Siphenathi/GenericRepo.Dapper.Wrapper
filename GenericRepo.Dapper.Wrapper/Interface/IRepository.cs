using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepo.Dapper.Wrapper.Interface
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(object id, string keyName);
		Task<IEnumerable<T>> GetAllAsync(object id, string keyName);
		Task<int> InsertAsync(T entity, params string[] namesOfColumnsToBeExcluded);
		Task<int> UpdateAsync(string keyName, T entity, params string[] namesOfColumnsToBeExcluded);
		Task<int> DeleteAsync(object id, string keyName);
	}
}