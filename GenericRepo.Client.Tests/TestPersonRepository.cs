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
		public async Task GetPerson_WhenCalled_ShouldReturnPerson()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetPersonAsync(1);

			//Assert
			actual.Should().NotBeNull();
		}

		[Test]
		public void GetPerson_WhenCalledWithNonExistingPersonCode_ShouldThrowException()
		{
			//Arrange
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);

			//Act
			var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => sut.GetPersonAsync(1264949526));

			//Assert
			Assert.AreEqual("dbo.Person with Code [1264949526] could not be found.", exception.Message);
		}

		[Test]
		public async Task InsertOrUpdatePerson_WhenCalledWithNonExistingPerson_ShouldSavePerson()
		{
			const int expectedNumberOfRowsToBeAffected = 1;
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var person = new Person
			{
				Name = "Person1",
				Surname = "Person1",
				Id_Number = "9501045305082"
			};

			//Act
			var actual = await sut.InsertOrUpdatePersonAsync(person);

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
			var actual = await sut.AddPersonAsync(person);

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
				Name = "Nathi",
				Surname = "Pantshwa Hlanga",
				Id_Number = "9x01045404082"
			};

			//act
			var actual = await sut.UpdatePersonAsync(person);

			//Assert
			actual.Should().Be(1);
		}

		[Test]
		public void UpdatePerson_WhenCalledWithNonExistentPerson_ShouldThrowArException()
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
			var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => sut.UpdatePersonAsync(person));

			//Assert
			Assert.AreEqual("dbo.Person with Code [7000] could not be found.", exception.Message);
		}

		[Test]
		public void DeletePerson_WhenCalledWithNonExistentCode_ShouldThrowException()
		{
			//Arrange 
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var code = 1000;

			//Act
			var actual = Assert.ThrowsAsync<KeyNotFoundException>(() => sut.DeletePersonAsync(code));

			//Assert
			actual.Message.Should().Be("dbo.Person with Code [1000] could not be found.");
		}

		[Test]
		public async Task DeletePerson_WhenCalledWithCode_ShouldDeletePerson()
		{
			//Arrange 
			var sut = CreatePersonRepository(ConnectionString, DatabaseProvider);
			var numberOfRowsAffected = 1;
			var code = 72;

			//Act
			var actual = await sut.DeletePersonAsync(code);

			//Assert
			actual.Should().Be(numberOfRowsAffected);
		}

		private static IPersonRepository CreatePersonRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			IPersonRepository personRepository = new PersonRepository(connectionString, databaseProvider);
			return personRepository;
		}
	}
}