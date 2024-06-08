using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepo.Client.Model;

namespace GenericRepo.Client
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllUserAsync();
		Task<User> GetUserAsync(string id);
		Task<int> AddUserAsync(User user);
		Task<int> UpdateUserAsync(User user);
		Task<int> DeleteUserAsync(string id);
	}
}