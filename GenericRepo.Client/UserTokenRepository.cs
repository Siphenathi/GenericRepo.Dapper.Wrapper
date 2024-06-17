using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepo.Client.Model;
using GenericRepo.Dapper.Wrapper;
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;

namespace GenericRepo.Client
{
	public class UserTokenRepository : IUserTokenRepository
	{
		private readonly IRepository<UserToken> _userTokenRepository;
		private const string TableName = "UserTokens";
		public UserTokenRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			_userTokenRepository = new Repository<UserToken>(TableName, connectionString, databaseProvider);
		}

		public async Task<UserToken> GetUserTokenAsync(Dictionary<string, object> dictionary)
		{
			return await _userTokenRepository.GetAsync(dictionary);
		}
	}
}