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
		private const string _tableName = "Persons";
		private const string _primaryKeyName = "Code";
		public PersonRepository(string connectionString)
		{
			_personRepository = new Repository<Person>(_tableName, connectionString);
		}

		public async Task<IEnumerable<Person>> GetAllPeopleAsync()
		{
			return await _personRepository.GetAllAsync();
		}

		public async Task<Person> GetPersonAsync(int code)
		{
			return await _personRepository.GetAsync(code, _primaryKeyName);
		}

		public async Task<int> AddPersonAsync(Person person)
		{
			return await _personRepository.InsertAsync(person, _primaryKeyName);
		}

		public async Task<int> UpdatePersonAsync(Person person)
		{
			var numberOfRowsAffected = await _personRepository.UpdateAsync(_primaryKeyName, person, _primaryKeyName, "Id_Number");
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{_tableName[0..^1]} with {_primaryKeyName} [{person.Code}] could not be found.");
			return numberOfRowsAffected;
		}

		public async Task<int> DeletePersonAsync(int code)
		{
			var numberOfRowsAffected = await _personRepository.DeleteAsync(code, _primaryKeyName);
			if (numberOfRowsAffected == 0) throw new KeyNotFoundException($"{_tableName[0..^1]} with {_primaryKeyName} [{code}] could not be found.");
			return numberOfRowsAffected;
		}
	}
}
