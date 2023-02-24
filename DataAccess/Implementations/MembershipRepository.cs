using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
	public class MembershipRepository : BaseRepository<MembershipStatus>
	{

		public MembershipRepository(DbContextOptions<AppDbContext> options, ILogger<BaseRepository<MembershipStatus>> logger) : base(options, logger)
		{
			_dbSet = MembershipStatuses;
		}

		public async Task<MembershipStatus> GetStatusForCustomerAsync(int customerId)
		{
			return await _dbSet.SingleOrDefaultAsync(t => t.CustomerId == customerId);
		}

        public MembershipStatus GetStatusForCustomer(int customerId)
        {
            return _dbSet.SingleOrDefault(t => t.CustomerId == customerId);
        }

        public void UpdateMemebershipStatus(int customerId, Purchase purchase)
		{
			var customerStatus = GetStatusForCustomer(customerId);
			if (customerStatus == null)
			{
				customerStatus = Create(new MembershipStatus() { CustomerId = customerId });
			}
			foreach(var order in purchase.Orders)
			{
				switch (order.OrderItem.OrderItemType)
				{
					case Models.Enums.OrderItemType.BookClubMembeship:
						customerStatus.BookClubMembershiExpirationDate = (order.OrderItem as IMembershipOrderItem).ExpirationDate;
						break;
					case Models.Enums.OrderItemType.VideoClubMembership:
						customerStatus.VideoClubMembershiExpirationDate = (order.OrderItem as IMembershipOrderItem).ExpirationDate;
						break;
					case Models.Enums.OrderItemType.Premium:
						customerStatus.PremiumMembershiExpirationDate = (order.OrderItem as IMembershipOrderItem).ExpirationDate;
						break;
					default:
						break;
				}
			}
			Update(customerStatus);
		}
	}
}
