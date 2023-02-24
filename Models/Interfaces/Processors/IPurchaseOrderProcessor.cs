using Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Processors
{
	public interface IPurchaseOrderProcessor
	{
        Task<PurchaseOrderProcessResult> ProcessPurchaseAsync(Purchase purchase);
    }
}
