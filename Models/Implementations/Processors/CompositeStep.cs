using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Implementations.Processors
{
	public class CompositeStep : PurchaseProcessingStep
	{
		public readonly List<PurchaseProcessingStep> Steps = new List<PurchaseProcessingStep>();

		public void Add(PurchaseProcessingStep step)
		{
			Steps.Add(step);
		}

		public override PurchaseOrderProcessResult ProcessPurchase(Purchase purchase)
		{
			var result = new PurchaseOrderProcessResult
			{
				Success = true
			};

			foreach (var step in Steps)
			{
				var stepResult = step.ProcessPurchase(purchase);
				result.Success &= stepResult.Success;
				result.MembershipStatus = result.MembershipStatus ?? stepResult.MembershipStatus;
				result.ShippingSlips.AddRange(stepResult.ShippingSlips);
				result.ErrorMessages.AddRange(stepResult.ErrorMessages);
			}

			return result;
		}
	}
}

