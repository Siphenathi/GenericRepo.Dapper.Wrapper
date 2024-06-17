using FluentAssertions;
using GenericRepo.Client.Interface;
using GenericRepo.Client.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using GenericRepo.Dapper.Wrapper.Domain;

namespace GenericRepo.Client.Tests
{
	[TestFixture]
	public class TestPersonService
	{
		private const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=AMSDatabase";
		private const DatabaseProvider DatabaseProvider = Dapper.Wrapper.Domain.DatabaseProvider.MsSql;
		private TransactionScope _scope;

		[SetUp]
		public void SetUp()
		{
			_scope = new TransactionScope();
		}

		[TearDown]
		public void TearDown()
		{
			_scope.Dispose();
		}

		[Test]
		public async Task GetAllPeople_WhenCalled_ShouldReturnAllPeople()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetAllPeopleAsync();
			var dataLength = actual.Count();

			//Assert
			Assert.IsTrue(dataLength > 0, "Database must not be empty");
		}

		[Test]
		public async Task GetPerson_WhenCalledWithPersonCode_ShouldReturnPerson()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetPersonAsync(new Dictionary<string, object>
			{
				{"code", 1 }
			});

			//Assert
			actual.Should().NotBeNull();
		}

		[Test]
		public async Task GetPerson_WhenCalledWithNonExistingPersonCode_ShouldThrowException()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetPersonAsync(new Dictionary<string, object>
			{
				{"code", 1023 }
			});

			//Assert
			actual.Should().BeNull();
		}

		[Test]
		public async Task InsertOrUpdatePerson_WhenCalledWithNonExistingPerson_ShouldSavePerson()
		{
			const int expectedNumberOfRowsToBeAffected = 1;
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var person = new Person
			{
				Code = 75,
				Name = "Person1",
				Surname = "Person1",
				Id_Number = "950GO45305082"
			};

			//Act
			var actual = await sut.InsertOrUpdatePersonAsync(new Dictionary<string, object>
			{
				{"code", 75 }
			}, person, "code");

			//Assert
			actual.Should().Be(expectedNumberOfRowsToBeAffected);
		}

		[Test]
		public async Task AddPerson_WhenCalledWithPerson_ShouldSavePerson()
		{
			var expectedNumberOfRowsToBeAffected = 1;
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var person = new Person
			{
				Name = "Siphenathi",
				Surname = "Pantshwa",
				Id_Number = "9501045507082"
			};

			//Act
			var actual = await sut.AddPersonAsync(person, "code");

			//Assert
			actual.Should().Be(expectedNumberOfRowsToBeAffected);
		}

		[Test]
		public async Task UpdatePerson_WhenCalledWithExistingPerson_ShouldUpdatePerson()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var person = new Person
			{
				Code = 72,
				Name = "Sindisiwe Zinhle",
				Surname = "Kubheka",
				Id_Number = "9x01045404082"
			};

			//act
			var actual = await sut.UpdatePersonAsync(new Dictionary<string, object>
			{
				{"code", 72 }
			}, person, "Code");

			//Assert
			actual.Should().Be(1);
		}

		[Test]
		public async Task UpdatePerson_WhenCalledWithNonExistentPerson_ShouldNotUpdateTable()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var person = new Person
			{
				Code = 7000,
				Name = "Nathi",
				Surname = "Pantshwa Hlanga",
				Id_Number = "9x01045707082"
			};

			//act
			var actual = await sut.UpdatePersonAsync(new Dictionary<string, object>
			{
				{"Code", 7000 }
			}, person, "code");

			//Assert
			actual.Should().Be(0);
		}

		[Test]
		public async Task DeletePerson_WhenCalledWithNonExistentCode_ShouldThrowException()
		{
			//Arrange 
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.DeletePersonAsync(new Dictionary<string, object>
			{
				{"Code", 7000 }
			});

			//Assert
			actual.Should().Be(0);
		}

		[Test]
		public async Task DeletePerson_WhenCalledWithCode_ShouldDeletePerson()
		{
			//Arrange 
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.DeletePersonAsync(new Dictionary<string, object>
			{
				{"Code", 72 }
			});

			//Assert
			actual.Should().Be(1);
		}

		private static IPersonRepository CreatePersonRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			IPersonRepository personRepository = new PersonRepository(connectionString, databaseProvider);
			return personRepository;
		}
	}
}