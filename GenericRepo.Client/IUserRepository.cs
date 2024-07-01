using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepo.Client.Model;

namespace GenericRepo.Client
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllUserAsync();
		Task<IEnumerable<User>> GetAllUserAsync(Dictionary<string, object> parameters);
		Task<User> GetUserAsync(Dictionary<string, object> parameters);
		Task<int> AddUserAsync(User user, params string[] namesOfColumnsToBeExcluded);
		Task<int> UpdateUserAsync(Dictionary<string, object> parameters, User user, params string[] namesOfColumnsToBeExcluded);
		Task<int> DeleteUserAsync(Dictionary<string, object> parameters);
	}
}