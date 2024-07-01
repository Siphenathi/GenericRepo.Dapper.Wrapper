using System;
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
		public async Task GetAllUser_WhenCalledListOfParameters_ShouldReturnAllUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetAllUserAsync(new Dictionary<string, object>
			{
				{"Id","1cdaf7fe-0fab-42ca-ad1a-3ef6eb9201dd"},
				{"FirstName", "Tandile"}
			});

			//Assert
			Assert.IsTrue(actual.Any(), "Database must not be empty");
		}

		[Test]
		public async Task GetUser_WhenCalledUserKey_ShouldReturnUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetUserAsync(new Dictionary<string, object>
			{
				{"Id","1cdaf7fe-0fab-42ca-ad1a-3ef6eb9201dd"}
			});

			//Assert
			actual.Should().NotBeNull();
		}

		[Test]
		public async Task GetUser_WhenCalledWithUsername_ShouldReturnUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.GetUserAsync(new Dictionary<string, object>
			{
				{"username","tandile"}
			});

			//Assert
			actual.Should().NotBeNull();
		}

		[Test]
		[Ignore("Transaction scope is not working")]
		public async Task AddUser_WhenCalledWithUser_ShouldSaveUser()
		{
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
			actual.Should().Be(1);
		}

		[Test]
		[Ignore("Transaction scope is not working")]
		public async Task UpdateUser_WhenCalledWithExistingUser_ShouldUpdateUser()
		{
			//Arrange
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);
			var user = new User
			{
				Id = "9af79d4d-4be3-4431-a798-62363b380a5e",
				FirstName = "Siphenathi",
				LastNames = "Pantshwa",
				IdNumber = "9500000000000000",
				Email = "spantshwa.lukho@gmail.com",
				EntryDate = DateTime.Now,
				NormalizedUserName = "SPANTSHWA",
				UserName = "spantshwa"
			};

			//act
			var actual = await sut.UpdateUserAsync(new Dictionary<string, object>
			{
				{"Id","9af79d4d-4be3-4431-a798-62363b380a5e"}
			}, user, "Id");

			//Assert
			actual.Should().Be(1);
		}

		[Test]
		public async Task UpdateUser_WhenCalledWithNonExistentUser_ShouldNotUpdateUser()
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
			var actual = await sut.UpdateUserAsync(new Dictionary<string, object>
			{
				{"Id","9af79d4d-4be3-4431-a798-62363b380a5e"}
			}, user, "Id");

			//Assert
			actual.Should().Be(0);
		}

		[Test]
		public async Task DeleteUser_WhenCalledWithNonExistentUser_ShouldNotDeleteUser()
		{
			//Arrange 
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//act
			var actual = await sut.DeleteUserAsync(new Dictionary<string, object>
			{
				{"Id","9af79d4d-4be3-4431-a798-62363b380a5e"}
			});

			//Assert
			actual.Should().Be(0);
		}

		[Test]
		[Ignore("Transaction scope is not working")]
		public async Task DeleteUser_WhenCalledWithCode_ShouldDeleteUser()
		{
			//Arrange 
			var sut = CreateUserRepository(ConnectionString, DatabaseProvider);

			//Act
			var actual = await sut.DeleteUserAsync(new Dictionary<string, object>
			{
				{"Id","ea9c3087-2d3e-4e91-9050-3cde3ce7c995"}
			});

			//Assert
			actual.Should().Be(1);
		}

		private static IUserRepository CreateUserRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			IUserRepository userRepository = new UserRepository(connectionString, databaseProvider);
			return userRepository;
		}
	}
}