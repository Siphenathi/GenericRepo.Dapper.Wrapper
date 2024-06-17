using System;

namespace GenericRepo.Client.Model
{
	public class UserToken
	{
		public string UserId { get; set; }
		public string LoginProvider { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public DateTime EntryDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		public bool Active { get; set; }
	}
}