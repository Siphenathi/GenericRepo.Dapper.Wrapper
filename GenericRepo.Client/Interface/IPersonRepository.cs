using GenericRepo.Client.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepo.Client.Interface
{
	public interface IPersonRepository
	{
		Task<int> InsertOrUpdatePersonAsync(Dictionary<string, object> keys, Person person, params string[] namesOfColumnsToBeExcluded);
		Task<int> AddPersonAsync(Person person, params string[] namesOfColumnsToBeExcluded);
		Task<int> DeletePersonAsync(Dictionary<string, object> keys);
		Task<IEnumerable<Person>> GetAllPeopleAsync();
		Task<Person> GetPersonAsync(Dictionary<string, object> keys);
		Task<int> UpdatePersonAsync(Dictionary<string, object> keys, Person person, params string[] namesOfColumnsToBeExcluded);
	}
}