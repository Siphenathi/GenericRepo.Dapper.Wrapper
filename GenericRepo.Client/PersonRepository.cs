using GenericRepo.Client.Interface;
using GenericRepo.Client.Model;
using GenericRepo.Dapper.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericRepo.Client
{
	public class PersonRepository : IPersonRepository
	{
		private readonly IRepository<Person> _personRepository;
		private const string TableName = "Persons";
		private const string PrimaryKeyName = "Code";
		public PersonRepository(string connectionString)
		{
			_personRepository = new Repository<Person>(TableName, connectionString);
		}

		public async Task<IEnumerable<Person>> GetAllPeopleAsync()
		{
			return await _personRepository.GetAllAsync();
		}

		public async Task<Person> GetPersonAsync(int code)
		{
			return await _personRepository.GetAsync(code, PrimaryKeyName);
		}

		public async Task<int> AddPersonAsync(Person person)
		{
			return await _personRepository.InsertAsync(person, PrimaryKeyName);
		}

		public async Task<int> UpdatePersonAsync(Person person)
		{
			var numberOfRowsAffected = await _personRepository.UpdateAsync(PrimaryKeyName, person, "Id_Number");
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{TableName[..^1]} with {PrimaryKeyName} [{person.Code}] could not be found.");
			return numberOfRowsAffected;
		}

		public async Task<int> DeletePersonAsync(int code)
		{
			var numberOfRowsAffected = await _personRepository.DeleteAsync(code, PrimaryKeyName);
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{TableName[..^1]} with {PrimaryKeyName} [{code}] could not be found.");
			return numberOfRowsAffected;
		}
	}
}
