using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Interfaces
{
	public interface IRealProductOrderItem : IOrdersItem
	{
		float MembershipDiscount { get; set; }
	}
}
