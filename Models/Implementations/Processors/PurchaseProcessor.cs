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
        private PurchaseOrderProcessResult result;
        private List<Action<Purchase>> Steps = new List<Action<Purchase>>();

        public PurchaseProcessor(IMembershipActivationService membershipActivationService, IShippingService shippingService)
        {
            _membershipActivationService = membershipActivationService;
            _shippingService = shippingService;
        }

		public void AddMembershipActivation()
		{
			Steps.Add((purchase) =>
			{
				try
				{
					result.MembershipStatus = _membershipActivationService.ActivateMembershipAsync(purchase);
                    if(result.MembershipStatus == null) {
                        result.Success = false;
                    }
				}
				catch (Exception ex)
				{
					result.Success = false;
					result.ErrorMessages.Add($"Failed to activate membership");
				}
			});



		}

		public void AddShippingActivation() {
            Steps.Add((purchase) =>
            {
                foreach (var item in purchase.Orders.Where(t => t.OrderItem.OrderItemType == Enums.OrderItemType.Book || t.OrderItem.OrderItemType == Enums.OrderItemType.Video))
                {
                    var shippingResult = _shippingService.GenerateShippingSlip(purchase.CustomerId, item);
                    if (shippingResult != null)
                        result.ShippingSlips.Add(shippingResult);
                    else
                    {
                        result.ErrorMessages.Add($"Failed generate Shipping Slip for item: [{item.OrderItem.NameOfItem}]");
                        result.Success = false;
                    }

                }
            });
           
        }

   

        public async Task<PurchaseOrderProcessResult> ProcessPurchaseAsync(Purchase purchase)
        {
            if (purchase == null)
            {
                throw new ArgumentNullException(nameof(purchase));
            }

            if (purchase.Orders == null || !purchase.Orders.Any())
            {
                throw new InvalidOperationException("Purchase order must contain at least one item");
            }
            result = new PurchaseOrderProcessResult()
            {
                Success = true
            }; ;
            
            
           foreach(var step in Steps) {
                if(result.Success) 
                    step(purchase);
            }

            if (result.Success)
                _membershipActivationService.CompleteTransaction(result.MembershipStatus);
           
            return result;
            
        }
    }
}
