
using System.Collections.Generic;

namespace Models.Interfaces
{
	public interface ICustomer : IEntity
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		string Address { get; set; }
		List<Purchase> Purchases { get; set; }
		MembershipStatus MembershipStatus { get; set;}
	}
}