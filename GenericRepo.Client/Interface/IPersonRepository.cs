using GenericRepo.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepo.Client.Interface
{
	public interface IPersonRepository
	{
		Task<int> AddPersonAsync(Person person);
		Task<int> DeletePersonAsync(int code);
		Task<IEnumerable<Person>> GetAllPeopleAsync();
		Task<Person> GetPersonAsync(int code);
		Task<int> UpdatePersonAsync(Person person);
	}
}
