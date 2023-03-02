using DataAccess.Implementations;
using DataAccess.Interfaces;
using Models;
using Models.Interfaces;
using Models.Interfaces.Api;
using Models.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipService.Managers
{

	public class MembershipActivationService : IMembershipActivationService
	{
		private readonly MembershipManager _manager;
		private readonly ICustomerApi _customerApi;

		public MembershipActivationService(IRepository<MembershipStatus> repo, ICustomerApi api)
		{
			_manager = new MembershipManager(repo as MembershipRepository);
			_customerApi = api;
		}

		public MembershipStatus ActivateMembership(Purchase purchase)
		{
			var membershipItems = purchase.Orders.Where(il => il.OrderItem.OrderItemType == Models.Enums.OrderItemType.BookClubMembeship 
				|| il.OrderItem.OrderItemType == Models.Enums.OrderItemType.VideoClubMembership
				|| il.OrderItem.OrderItemType == Models.Enums.OrderItemType.Premium).Select(t => t.OrderItem);
			if (membershipItems == null || !membershipItems.Any()) return null;

			var customer = _customerApi.GetCustomerById(purchase.CustomerId);
			var existingMembershipStatus = _manager.GetMembershipForCustomer(purchase.CustomerId);
			if (existingMembershipStatus == null)
			{
				existingMembershipStatus = new MembershipStatus()
				{
					CustomerId = purchase.CustomerId
				};
			}

			foreach (IMembershipOrderItem membershipItem in membershipItems)
			{
				switch (membershipItem.OrderItemType)
				{
					case Models.Enums.OrderItemType.BookClubMembeship:
						existingMembershipStatus.BookClubMembershiExpirationDate = membershipItem.ExpirationDate;
						break;
					case Models.Enums.OrderItemType.VideoClubMembership:
						existingMembershipStatus.VideoClubMembershiExpirationDate = membershipItem.ExpirationDate;
						break;
					case Models.Enums.OrderItemType.Premium:
						existingMembershipStatus.PremiumMembershiExpirationDate = membershipItem.ExpirationDate;
						break;
				}
			}
			return existingMembershipStatus;
			
		}
        public void CompleteTransaction(MembershipStatus membershipStatus)
        {
            _manager.AddOrUpdateMembershipForCustomer(membershipStatus);
        }
    }

	
}

