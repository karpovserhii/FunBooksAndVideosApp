using FunBooksAndVideosApp.Models.Orders;
using Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Models
{
	public class Purchase : IPurchase
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Total { get; set; }
        public List<OrderPosition> Orders { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [JsonIgnore]
		public Customer Customer { get; set; }
	}
}