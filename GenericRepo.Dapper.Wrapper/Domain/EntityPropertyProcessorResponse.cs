using System;
using System.Collections.Generic;
using System.Text;

namespace GenericRepo.Dapper.Wrapper.Domain
{
	public class EntityPropertyProcessorResponse
	{
		public string Result { get; set; }
		public Error Error { get; set; }
	}
}
