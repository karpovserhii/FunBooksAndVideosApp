using Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
	public class Customer : ICustomer
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        [InverseProperty("Customer")]
        [JsonIgnore]
        public List<Purchase> Purchases {get;set;}
		public MembershipStatus MembershipStatus { get; set; }
		public string Address { get; set; }
	}
}