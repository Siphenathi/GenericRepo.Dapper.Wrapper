using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepo.Client.Model;

namespace GenericRepo.Client
{
	public interface IUserTokenRepository
	{
		Task<UserToken> GetUserTokenAsync(Dictionary<string, object> dictionary);
	}
}