using System;
using System.Collections.Generic;
using FluentAssertions;
using GenericRepo.Dapper.Wrapper.Tests.Model;
using GenericRepo.Dapper.Wrapper.Utilities;
using NUnit.Framework;

namespace GenericRepo.Dapper.Wrapper.Tests
{
	[TestFixture]
	public class TestDataTableProcessor
	{
		[Test]
		public void MapToDataTable_GivenEmptyList_ShouldEmptyDataTable()
		{
			//--------------Arrange--------------
			var emptyList = new List<RegisterEmployeeModel>();

			//--------------act--------------
			var actual = DataTableProcessor.MapToDataTable(emptyList);

			//--------------Assert--------------
			actual.Rows.Count.Should().Be(0);
		}

		[Test]
		public void MapToDataTable_GivenValidList_ShouldMapItToDataTable()
		{
			//--------------Arrange--------------
			var data = ListOfEmployees();

			//--------------act--------------
			var actual = DataTableProcessor.MapToDataTable(data);

			//--------------Assert--------------
			actual.Rows.Count.Should().Be(10000);
			actual.Columns.Count.Should().Be(4);
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void MapToDataTable_GivenInvalidInput_ShouldThrowAnException(string jsonObject)
		{
			//--------------Arrange----------
			
			//--------------Act--------------
			var exception = Assert.Throws<ArgumentNullException>(() => DataTableProcessor.MapToDataTable(jsonObject));

			//--------------Assert-----------
			exception.Message.Should().Be("Value cannot be null. (Parameter 'jsonObject')");
		}

		[Test]
		public void MapToDataTable_GivenValidInput_ShouldMapToDataTable()
		{
			//-------------------Arrange----------------------
			var json = GetJsonData();

			//-------------------Act--------------------------
			var dataTable = DataTableProcessor.MapToDataTable(json);

			//-------------------Assert-----------------------
			dataTable.Rows.Count.Should().Be(10);
		}

		private static List<RegisterEmployeeModel> ListOfEmployees()
		{
			var employeesToRegisters = new List<RegisterEmployeeModel>();
			var jobTitleId = 1;
			for (var count = 1; count <= 10000; count++)
			{
				if (jobTitleId == 4)
					jobTitleId = 1;

				employeesToRegisters.Add(new RegisterEmployeeModel
				{
					Name = $"name {count}",
					Surname = $"Surname {count}",
					JobTitleId = jobTitleId
				});
				jobTitleId++;
			}
			return employeesToRegisters;
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
	}
}
