using Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
	public class OrderPosition : IOrder
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public int Quantity { get; set; }

		public double Total { get; set; }
		public int PurchaseId { get; set; }

		public int OrderItemId { get; set; }
		[ForeignKey(nameof(OrderItemId))]
		public OrderItem OrderItem { get; set; }
	}
}
