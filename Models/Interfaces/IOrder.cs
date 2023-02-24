using System.Collections.Generic;

namespace Models.Interfaces
{
	public interface IOrder : IEntity
	{
		double Total { get; set; }
        int PurchaseId { get; set; }
        int OrderItemId { get; set; }
        int Quantity { get; set; }
    }
}