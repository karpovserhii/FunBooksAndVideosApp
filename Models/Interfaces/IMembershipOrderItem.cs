using System;

namespace Models.Interfaces
{
	public interface IMembershipOrderItem : IOrdersItem
	{
		DateTime? ExpirationDate { get; set; }
	}
}
