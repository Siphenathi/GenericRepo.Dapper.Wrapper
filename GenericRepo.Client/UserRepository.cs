using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepo.Client.Model;
using GenericRepo.Dapper.Wrapper;
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;

namespace GenericRepo.Client
{
	public class UserRepository : IUserRepository
	{
		private readonly IRepository<User> _userRepository;
		private const string TableName = "Users";
		public UserRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			_userRepository = new Repository<User>(TableName, connectionString, databaseProvider);
		}

		public async Task<IEnumerable<User>> GetAllUserAsync()
		{
			return await _userRepository.GetAllAsync();
		}

		public async Task<IEnumerable<User>> GetAllUserAsync(Dictionary<string, object> parameters)
		{
			return await _userRepository.GetAllAsync(parameters);
		}

		public async Task<User> GetUserAsync(Dictionary<string, object> parameters)
		{
			return await _userRepository.GetAsync(parameters);
		}

		public async Task<int> AddUserAsync(User user, params string[] namesOfColumnsToBeExcluded)
		{
			return await _userRepository.InsertAsync(user);
		}

		public async Task<int> UpdateUserAsync(Dictionary<string, object> parameters, User user, params string[] namesOfColumnsToBeExcluded)
		{
			return await _userRepository.UpdateAsync(parameters, user, namesOfColumnsToBeExcluded);
		}

		public async Task<int> DeleteUserAsync(Dictionary<string, object> parameters)
		{
			return await _userRepository.DeleteAsync(parameters);
		}
	}
}