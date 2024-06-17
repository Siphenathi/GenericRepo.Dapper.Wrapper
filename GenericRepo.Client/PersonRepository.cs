using GenericRepo.Client.Interface;
using GenericRepo.Client.Model;
using GenericRepo.Dapper.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;

namespace GenericRepo.Client
{
	public class PersonRepository : IPersonRepository
	{
		private readonly IRepository<Person> _personRepository;
		private const string TableName = "dbo.Persons";
		public PersonRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			_personRepository = new Repository<Person>(TableName, connectionString, databaseProvider);
		}

		public async Task<IEnumerable<Person>> GetAllPeopleAsync()
		{
			return await _personRepository.GetAllAsync();
		}

		public async Task<Person> GetPersonAsync(Dictionary<string, object> parameters)
		{
			return await _personRepository.GetAsync(parameters);
		}

		public async Task<int> InsertOrUpdatePersonAsync(Dictionary<string, object> parameters, Person person, params string[] namesOfColumnsToBeExcluded)
		{
			return await _personRepository.InsertOrUpdateAsync(parameters, person, namesOfColumnsToBeExcluded);
		}

		public async Task<int> AddPersonAsync(Person person, params string[] namesOfColumnsToBeExcluded)
		{
			return await _personRepository.InsertAsync(person, namesOfColumnsToBeExcluded);
		}

		public async Task<int> UpdatePersonAsync(Dictionary<string, object> parameters, Person person, params string[] namesOfColumnsToBeExcluded)
		{
			return await _personRepository.UpdateAsync(parameters, person, namesOfColumnsToBeExcluded);
		}

		public async Task<int> DeletePersonAsync(Dictionary<string, object> parameters)
		{
			return await _personRepository.DeleteAsync(parameters);
		}
	}
}