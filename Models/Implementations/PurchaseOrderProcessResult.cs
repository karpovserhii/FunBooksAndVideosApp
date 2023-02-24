using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Implementations
{
	public class PurchaseOrderProcessResult
	{
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public List<ShippingSlip> ShippingSlips { get; set; } = new List<ShippingSlip>();
        public MembershipStatus MembershipStatus { get; set; }
    }
}
