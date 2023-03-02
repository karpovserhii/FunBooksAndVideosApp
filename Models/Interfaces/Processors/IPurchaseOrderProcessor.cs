using Models.Implementations;
using Models.Implementations.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Processors
{
	public interface IPurchaseOrderProcessor
	{
		PurchaseOrderProcessResult ProcessPurchase(Purchase purchase);
		CompositeStep RootStep { get; }
	}
}
