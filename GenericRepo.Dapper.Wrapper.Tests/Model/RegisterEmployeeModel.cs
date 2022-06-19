using System;

namespace GenericRepo.Dapper.Wrapper.Tests.Model
{
	public class RegisterEmployeeModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public int JobTitleId { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}