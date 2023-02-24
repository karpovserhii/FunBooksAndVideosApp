using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class ShippingSlip
	{

		public Customer Customer { get; set; }
		public int PurchaseId { get; set; }
		public OrderItem ShippingItem { get; set; }
		
	}
}
