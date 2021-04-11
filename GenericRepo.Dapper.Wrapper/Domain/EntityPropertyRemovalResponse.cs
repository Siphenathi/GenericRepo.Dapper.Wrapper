using System;
using System.Collections.Generic;
using System.Text;

namespace GenericRepo.Dapper.Wrapper.Domain
{
	public class EntityPropertyRemovalResponse
	{
		public List<string> Properties { get; set; }
		public Error Error { get; set; }
	}
}
