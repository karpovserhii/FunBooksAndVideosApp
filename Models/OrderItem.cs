using Models.Enums;
using Models.Implementations.Books;
using Models.Implementations.Video;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class OrderItem : IBookOrderItem, IVideoOrderItem, IMembershipOrderItem
	{
		public OrderItemType OrderItemType { get; set; }
		public string NameOfItem { get; set; }
		public float Price { get; set; }
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public float MembershipDiscount { get; set; }
		public string Caption { get; set; }
		public string Author { get; set; }
		public TimeSpan? Length { get; set; }
		public string Title { get; set; }
		public DateTime? PublicationDate { get; set; }
	}
}
