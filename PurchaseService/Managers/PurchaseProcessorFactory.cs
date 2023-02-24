using DataAccess.Interfaces;
using MembershipService.Managers;
using Models;
using Models.Implementations.Processors;
using Models.Interfaces.Processors;
using Models.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseService.Managers
{
	public class PurchaseProcessorFactory
	{
        private readonly IMembershipActivationService _membershipActivationService;
        private readonly IShippingService _shippingService;

        public PurchaseProcessorFactory(
            IMembershipActivationService membershipActivationService, IShippingService shippingService)
        {
            _membershipActivationService = membershipActivationService;
            _shippingService = shippingService;
        }

		public  IPurchaseOrderProcessor CreatePurchaseOrderProcessor(Purchase purchase)
		{

			var processor = new PurchaseProcessor(_membershipActivationService, _shippingService);
            if(purchase.Orders.Any(t => t.OrderItem.OrderItemType == Models.Enums.OrderItemType.VideoClubMembership 
                || t.OrderItem.OrderItemType == Models.Enums.OrderItemType.BookClubMembeship
                || t.OrderItem.OrderItemType == Models.Enums.OrderItemType.Premium))
            {
                processor.AddMembershipActivation();
            }
            if (purchase.Orders.Any(t => t.OrderItem.OrderItemType == Models.Enums.OrderItemType.Book || t.OrderItem.OrderItemType == Models.Enums.OrderItemType.Video))
            {
                processor.AddShippingActivation();
            }

			return processor;
		}
	}
}

