using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Models.Interfaces.Api
{
	public interface IMembershipApi
	{
		
		[Get("/memberships")]
		Task<List<MembershipStatus>> GetAllMemberships();
		[Get("/memberships/{customerId}")]
		Task<MembershipStatus> GetMembershipStatusForCustomer(int customerId);
		[Post("/memberships/{customerId}")]
		Task CreateMembershipByOrder(int customerId, List<OrderItem> orderItems);
		
	}
}
