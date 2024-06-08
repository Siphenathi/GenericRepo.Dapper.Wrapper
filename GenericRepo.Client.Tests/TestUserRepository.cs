using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FluentAssertions;
using GenericRepo.Client.Model;
using GenericRepo.Dapper.Wrapper.Domain;
using NUnit.Framework;

namespace GenericRepo.Client.Tests
{
	public class TestUserRepository
	{
		private const string ConnectionString = "Server=localhost;port=3306;database=MarketHubAuthDb;user=root;password=Admin@01";
		private const DatabaseProvider DatabaseProvider = Dapper.Wrapper.Domain.DatabaseProvider.MySql;
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
		public async Task GetAllUser_WhenCalled_ShouldReturnAllUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetAllUserAsync();
			var dataLength = actual.Count();

			//Assert
			Assert.IsTrue(dataLength > 0, "Database must not be empty");
		}

		[Test]
		public async Task GetUser_WhenCalled_ShouldReturnUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetUserAsync("9af79d4d-4be3-4431-a798-62363b380a5y");

			//Assert
			actual.Should().NotBeNull();
		}

		[Test]
		public void GetUser_WhenCalledWithNonExistingUserCode_ShouldThrowException()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => sut.GetUserAsync("0f59895c-b3a3-48e9-b6d3-1e492e987bd1"));

			//Assert
			Assert.AreEqual("User with Id [0f59895c-b3a3-48e9-b6d3-1e492e987bd1] could not be found.", exception.Message);
		}

		[Test]
		[Ignore("Transaction scope is not working")]
		public async Task AddUser_WhenCalledWithUser_ShouldSaveUser()
		{
			var expectedNumberOfRowsToBeAffected = 1;
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);
			var user = new User
			{
				Id = "9af79d4d-4be3-4431-a798-62363b380a5e",
				FirstName = "Siphenathi",
				LastNames = "Pantshwa",
				IdNumber = "9501045404088",
				Email = "user@gmail.com"
			};

			//Act
			var actual = await sut.AddUserAsync(user);

			//Assert
			actual.Should().Be(expectedNumberOfRowsToBeAffected);
		}

		[Test]
		[Ignore("Transaction scope is not working")]
		public async Task UpdateUser_WhenCalledWithExistingUser_ShouldUpdateUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);
			var user = new User
			{
				Id = "0f59895c-b3a3-48e9-b6d3-1e492e987bd1",
				FirstName = "Siphenathi",
				LastNames = "Pantshwa",
				IdNumber = "9501045404088",
				Email = "user@gmail.com"
			};

			//act
			var actual = await sut.UpdateUserAsync(user);

			//Assert
			actual.Should().Be(1);
		}

		[Test]
		public void UpdateUser_WhenCalledWithNonExistentUser_ShouldThrowArException()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);
			var user = new User
			{
				Id = "0f59895c-b3a3-48e9-b6d3-1e492e987bd9",
				FirstName = "Siphenathi",
				LastNames = "Pantshwa",
				IdNumber = "9501045404088",
				Email = "user@gmail.com"
			};

			//act
			var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => sut.UpdateUserAsync(user));

			//Assert
			Assert.AreEqual("User with Id [0f59895c-b3a3-48e9-b6d3-1e492e987bd9] could not be found.", exception.Message);
		}

		[Test]
		public void DeleteUser_WhenCalledWithNonExistentCode_ShouldThrowException()
		{
			//Arrange 
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);
			const string id = "0f59895c-b3a3-48e9-b6d3-1e492e987bd3";

			//Act
			var actual = Assert.ThrowsAsync<KeyNotFoundException>(() => sut.DeleteUserAsync(id));

			//Assert
			actual.Message.Should().Be("User with Id [0f59895c-b3a3-48e9-b6d3-1e492e987bd3] could not be found.");
		}

		[Test]
		[Ignore("Transaction scope is not working")]
		public async Task DeleteUser_WhenCalledWithCode_ShouldDeleteUser()
		{
			//Arrange 
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);
			var numberOfRowsAffected = 1;
			var id = "ea9c3087-2d3e-4e91-9050-3cde3ce7c995";

			//Act
			var actual = await sut.DeleteUserAsync(id);

			//Assert
			actual.Should().Be(numberOfRowsAffected);
		}

		private static IUserRepository CreateUserRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			IUserRepository userRepository = new UserRepository(connectionString, databaseProvider);
			return userRepository;
		}
	}
}