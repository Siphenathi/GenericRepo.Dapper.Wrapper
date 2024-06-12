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
		private const string KeyName = "Id";
		public UserRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			_userRepository = new Repository<User>(TableName, connectionString, databaseProvider);
		}

		public async Task<IEnumerable<User>> GetAllUserAsync()
		{
			return await _userRepository.GetAllAsync();
		}

		public async Task<User> GetUserAsync(string id)
		{
			return await _userRepository.GetAsync(id, KeyName);
		}

		public async Task<User> GetUserByUsernameAsync(string username)
		{
			return await _userRepository.GetAsync(username, "username");
		}

		public async Task<int> AddUserAsync(User user)
		{
			return await _userRepository.InsertAsync(user);
		}

		public async Task<int> UpdateUserAsync(User user)
		{
			var numberOfRowsAffected = await _userRepository.UpdateAsync(KeyName, user, "Id");
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{TableName[..^1]} with {KeyName} [{user.Id}] could not be found.");
			return numberOfRowsAffected;
		}

		public async Task<int> DeleteUserAsync(string id)
		{
			var numberOfRowsAffected = await _userRepository.DeleteAsync(id, KeyName);
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{TableName[..^1]} with {KeyName} [{id}] could not be found.");
			return numberOfRowsAffected;
		}
	}
}