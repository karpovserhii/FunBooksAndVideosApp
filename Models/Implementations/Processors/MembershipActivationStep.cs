using Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Implementations.Processors
{
	public class MembershipActivationStep : PurchaseProcessingStep
	{
		private readonly IMembershipActivationService _membershipActivationService;

		public MembershipActivationStep(IMembershipActivationService membershipActivationService)
		{
			_membershipActivationService = membershipActivationService;
		}

		public override PurchaseOrderProcessResult ProcessPurchase(Purchase purchase)
		{
			var result = new PurchaseOrderProcessResult
			{
				Success = true
			};

			try
			{
				result.MembershipStatus = _membershipActivationService.ActivateMembership(purchase);
				if (result.MembershipStatus == null)
				{
					result.Success = false;
					result.ErrorMessages.Add($"Failed to activate membership for customer...");
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.ErrorMessages.Add($"Failed to activate membership, {ex.Message}");
			}

			return result;
		}
	}
}
