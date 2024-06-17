using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using FluentAssertions;
using GenericRepo.Dapper.Wrapper.Domain;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace GenericRepo.Client.Tests
{
	public class TestUserTokenRepository
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
		public void GetUserToken_WhenCalledWithInvalidKeys_ShouldThrowException()
		{
			//Arrange
			var sut = CreateUserTokenRepository(ConnectionString, DatabaseProvider);
			var keys = new Dictionary<string, object>
			{
				{" ","5y56y56y6y "}
			};

			//Act
			var exception = Assert.ThrowsAsync<MySqlException>(() => sut.GetUserTokenAsync(keys));

			//Assert
			exception.Message.Should().
				Contain("You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near");
		}

		[Test]
		public async Task GetUserToken_WhenCalledWithObjectOfKeys_ShouldReturnUserToken()
		{
			//Arrange
			var sut = CreateUserTokenRepository(ConnectionString, DatabaseProvider);
			var keys = new Dictionary<string, object>
			{
				{"UserId", "0f59895c-b3a3-48e9-b6d3-1e492e987bd1" },
				{"LoginProvider", "AuthService"},
				{"Name", "RefreshToken"}
			};

			//Act
			var actual = await sut.GetUserTokenAsync(keys);

			//Assert
			actual.Should().NotBeNull();
		}

		private static IUserTokenRepository CreateUserTokenRepository(string connectionString, DatabaseProvider databaseProvider)
		{
			return new UserTokenRepository(connectionString, databaseProvider);
		}
	}
}