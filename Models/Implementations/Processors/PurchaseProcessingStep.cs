using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Implementations.Processors
{
	public abstract class PurchaseProcessingStep
	{
		public abstract PurchaseOrderProcessResult ProcessPurchase(Purchase purchase);
	}
}
