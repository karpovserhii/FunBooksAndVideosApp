using Models.Interfaces;
using Models.Interfaces.Processors;
using Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Implementations.Processors
{
    public class PurchaseProcessor : IPurchaseOrderProcessor
    {
        private readonly IMembershipActivationService _membershipActivationService;
        private readonly IShippingService _shippingService;
        public CompositeStep RootStep { get; } = new CompositeStep();

		public PurchaseProcessor(IMembershipActivationService membershipActivationService, IShippingService shippingService)
        {
            _membershipActivationService = membershipActivationService;
            _shippingService = shippingService;
        }

        public PurchaseOrderProcessResult ProcessPurchase(Purchase purchase)
        {
            if(purchase == null)
            {
                throw new ArgumentNullException($"Purchase is not defined");
            }
            if(purchase.Orders?.Any() != true) {
                throw new ArgumentException("Purchase must contain order items");
            }
            if (purchase.Orders.Any(t => t.OrderItem.OrderItemType == Models.Enums.OrderItemType.VideoClubMembership
                || t.OrderItem.OrderItemType == Models.Enums.OrderItemType.BookClubMembeship
                || t.OrderItem.OrderItemType == Models.Enums.OrderItemType.Premium))
            {
				RootStep.Add(new MembershipActivationStep(_membershipActivationService));
			}
            if (purchase.Orders.Any(t => t.OrderItem.OrderItemType == Models.Enums.OrderItemType.Book || t.OrderItem.OrderItemType == Models.Enums.OrderItemType.Video))
            {
				RootStep.Add(new ShippingActivationStep(_shippingService));
			}


			var result = RootStep.ProcessPurchase(purchase);
			if (result.Success)
			{
				_membershipActivationService.CompleteTransaction(result.MembershipStatus);
			}

			return result;
            
        }
    }
}
