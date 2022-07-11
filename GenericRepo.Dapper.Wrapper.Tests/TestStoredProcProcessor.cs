using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using FluentAssertions;
using GenericRepo.Dapper.Wrapper.Interface;
using GenericRepo.Dapper.Wrapper.Tests.Model;
using GenericRepo.Dapper.Wrapper.Utilities;
using NUnit.Framework;

namespace GenericRepo.Dapper.Wrapper.Tests
{
	[TestFixture]
	public class TestStoredProcProcessor
	{
		public readonly string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
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

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void CreateStoredProcProcessor_GivenInvalidConnectionString_ShouldThrowException(string connectingString)
		{
			//Arrange
			//Act
			var exception = Assert.Throws<ArgumentNullException>(() =>
			{
				_ = new StoredProcProcessor(connectingString);
			});

			//Assert
			exception.Message.Should().Be("Value cannot be null. (Parameter 'connectionString')");
		}

		[Test]
		public void CreateStoredProcProcessor_GivenValidConnectionString_ShouldStoredProcProcessor()
		{
			//Arrange
			//Act
			var actual = new StoredProcProcessor(ConnectionString);

			//Assert
			actual.Should().BeOfType<StoredProcProcessor>();
		}

		[TestCase("")]
		[TestCase(null)]
		public void GetDataAsync_GivenInvalidProcNameAndParameter_ShouldThrowAnError(string procName)
		{
			//Arrange
			var parameters = new DynamicParameters();
			parameters.Add("@id", 3, DbType.Int32);
			var sut = CreateStoredProcProcessor();

			//Act
			var exception = Assert.ThrowsAsync<InvalidOperationException>(() => sut.GetDataAsync<EmployeeModel>(procName, parameters));

			//Assert
			exception.Message.Should().
				Contain("BeginExecuteReader: CommandText property has not been initialized");
		}

		[Test]
		public void GetDataAsync_GivenWhiteSpaceAsProcNameAndParameter_ShouldThrowAnError()
		{
			//Arrange
			var parameters = new DynamicParameters();
			parameters.Add("@id", 3, DbType.Int32);
			var sut = CreateStoredProcProcessor();

			//Act
			var exception = Assert.ThrowsAsync<SqlException>(() => sut.GetDataAsync<EmployeeModel>(" ", parameters));

			//Assert
			exception.Message.Should().
				Contain("Could not find stored procedure ' '.");
		}

		[Test]
		public async Task GetDataAsync_GivenProcNameAndParameters_ShouldReturnOneRecord()
		{
			//Arrange
			const string storedProcName = "PS_Employee";
			var parameters = new DynamicParameters();
			parameters.Add("@id", 3, DbType.Int32);
			var sut = CreateStoredProcProcessor();

			//Act
			var actual = await sut.GetDataAsync<EmployeeModel>(storedProcName, parameters);

			//Assert
			actual.Count().Should().Be(1);
		}

		[Test]
		public async Task ExecuteAsync_GivenInsertProcNameWithDynamicParameters_ShouldInsertNewRecord()
		{
			//Arrange
			const string storedProcName = "PI_Employee";
			var sut = CreateStoredProcProcessor();
			var parameters = new DynamicParameters();
			parameters.Add("@name", "Kungayo", DbType.String);
			parameters.Add("@surname", "Kubheka", DbType.String);
			parameters.Add("@dateOfBirth", DateTime.Now, DbType.DateTime);
			parameters.Add("@jobTitleId", 3, DbType.Int32);

			//Act
			var actual = await sut.ExecuteAsync(storedProcName, parameters);

			//Assert
			actual.Should().BeGreaterOrEqualTo(1);
		}

		[Test]
		public async Task ExecuteInBulkAsync_GivenInsertStoredProcAndObjectWithDataTable_ShouldInsertNewRecord()
		{
			//Arrange
			const string storedProcName = "PI_EmployeeTableType";
			var sut = CreateStoredProcProcessor();
			var dataTable = GetDataTable();
			var obj = new { employeeType = dataTable };

			//Act
			var actual = await sut.ExecuteInBulkAsync(storedProcName, obj);

			//Assert
			actual.Should().Be(3);
		}

		[Test]
		public async Task ExecuteInBulkAsync_GivenInsertStoredProcAndObjectCreatedWithDataTable_ShouldInsertNewRecord()
		{
			//Arrange
			const string storedProcName = "PI_EmployeeTableType";
			var sut = CreateStoredProcProcessor();
			var dataTable = DataTableProcessor.MapToDataTable(ListOfEmployees());
			var obj = new { employeeType = dataTable };

			//Act
			var actual = await sut.ExecuteInBulkAsync(storedProcName, obj);

			//Assert
			actual.Should().Be(100000);
		}

		[Test]
		public async Task ExecuteInBulkAsync_GivenInsertStoredProcAndObjectCreatedWithJsonData_ShouldInsertNewRecord()
		{
			//Arrange
			const string storedProcName = "PI_EmployeeTableType";
			var sut = CreateStoredProcProcessor();
			var dataTable = DataTableProcessor.MapToDataTable(GetJsonData());
			var obj = new { employeeType = dataTable };

			//Act
			var actual = await sut.ExecuteInBulkAsync(storedProcName, obj);

			//Assert
			actual.Should().Be(10);
		}

		private static string GetJsonData()
		{
			return "[{\"Name\":\"name 1\",\"Surname\":\"Surname 1\",\"JobTitleId\":1,\"DateOfBirth\":\"0001-01-01T00:00:00\"}" +
			       ",{\"Name\":\"name 2\",\"Surname\":\"Surname 2\",\"JobTitleId\":2,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 3\",\"Surname\":\"Surname 3\",\"JobTitleId\":3,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 4\",\"Surname\":\"Surname 4\",\"JobTitleId\":1,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 5\",\"Surname\":\"Surname 5\",\"JobTitleId\":2,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 6\",\"Surname\":\"Surname 6\",\"JobTitleId\":3,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 7\",\"Surname\":\"Surname 7\",\"JobTitleId\":1,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 8\",\"Surname\":\"Surname 8\",\"JobTitleId\":2,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 9\",\"Surname\":\"Surname 9\",\"JobTitleId\":3,\"DateOfBirth\":\"0001-01-01T00:00:00\"}," +
			       "{\"Name\":\"name 10\",\"Surname\":\"Surname 10\",\"JobTitleId\":1,\"DateOfBirth\":\"0001-01-01T00:00:00\"}]";
		}

		private static List<RegisterEmployeeModel> ListOfEmployees()
		{
			var employeesToRegisters = new List<RegisterEmployeeModel>();
			var jobTitleId = 1;
			for (var count = 1; count <= 100000; count++)
			{
				if (jobTitleId == 4)
					jobTitleId = 1;

				employeesToRegisters.Add(new RegisterEmployeeModel
				{
					Name = $"name {count}",
					Surname = $"Surname {count}",
					JobTitleId = jobTitleId,
					DateOfBirth = DateTime.Now
				});
				jobTitleId++;
			}
			return employeesToRegisters;
		}

		private static DataTable GetDataTable()
		{
			var dataTable = new DataTable();
			dataTable.Columns.AddRange(GetDataColumns());
			dataTable.Rows.Add("Siwa", "Pantshwa", 3, "1993-02-04");
			dataTable.Rows.Add("Tandile", "Pantshwa", 4, "1998-02-02");
			dataTable.Rows.Add("Saider", "Monelo", 1, "1995-09-05");
			return dataTable;
		}

		private static DataColumn [] GetDataColumns()
		{
			return new[] { new DataColumn("Name"), new DataColumn("Surname"),
				new DataColumn("JobTitleId"), new DataColumn("DateOfBirth") };
		}

		private IStoredProcProcessor CreateStoredProcProcessor()
		{
			return new StoredProcProcessor(ConnectionString);
		}
	}
}
