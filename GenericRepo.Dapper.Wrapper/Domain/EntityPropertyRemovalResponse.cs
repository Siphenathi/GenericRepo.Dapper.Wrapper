using System.Collections.Generic;

namespace GenericRepo.Dapper.Wrapper.Domain
{
	public class EntityPropertyRemovalResponse
	{
		public List<string> Properties { get; set; }
		public Error Error { get; set; }
	}
}
