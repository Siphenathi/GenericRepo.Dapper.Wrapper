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
		private const string PrimaryKeyName = "Id";
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
			return await _userRepository.GetAsync(id, PrimaryKeyName);
		}

		public async Task<int> AddUserAsync(User user)
		{
			return await _userRepository.InsertAsync(user);
		}

		public async Task<int> UpdateUserAsync(User user)
		{
			var numberOfRowsAffected = await _userRepository.UpdateAsync(PrimaryKeyName, user, "Id");
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{TableName[..^1]} with {PrimaryKeyName} [{user.Id}] could not be found.");
			return numberOfRowsAffected;
		}

		public async Task<int> DeleteUserAsync(string id)
		{
			var numberOfRowsAffected = await _userRepository.DeleteAsync(id, PrimaryKeyName);
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{TableName[..^1]} with {PrimaryKeyName} [{id}] could not be found.");
			return numberOfRowsAffected;
		}
	}
}