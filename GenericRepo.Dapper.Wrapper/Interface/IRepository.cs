using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepo.Dapper.Wrapper.Interface
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllAsync(Dictionary<string, object> parameters);
		Task<T> GetAsync(Dictionary<string, object> parameters);
		Task<int> InsertAsync(T entity, params string[] namesOfColumnsToBeExcluded);
		Task<int> UpdateAsync(Dictionary<string, object> parameters, T entity, params string[] namesOfColumnsToBeExcluded);
		Task<int> InsertOrUpdateAsync(Dictionary<string, object> parameters, T entity, params string[] namesOfColumnsToBeExcluded);
		Task<int> DeleteAsync(Dictionary<string, object> parameters);
	}
}