using Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Implementations.Processors
{
	public class ShippingActivationStep : PurchaseProcessingStep
	{
		private readonly IShippingService _shippingService;

		public ShippingActivationStep(IShippingService shippingService)
		{
			_shippingService = shippingService;
		}

		public override PurchaseOrderProcessResult ProcessPurchase(Purchase purchase)
		{
			var result = new PurchaseOrderProcessResult
			{
				Success = true
			};

			foreach (var item in purchase.Orders.Where(t => t.OrderItem.OrderItemType == Enums.OrderItemType.Book || t.OrderItem.OrderItemType == Enums.OrderItemType.Video))
			{
				var shippingResult = _shippingService.GenerateShippingSlip(purchase.CustomerId, item);
				if (shippingResult != null)
				{
					result.ShippingSlips.Add(shippingResult);
				}
				else
				{
					result.ErrorMessages.Add($"Failed generate Shipping Slip for item: [{item.OrderItem.NameOfItem}]");
					result.Success = false;
				}
			}

			return result;
		}
	}
}
