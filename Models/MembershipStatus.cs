using Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class MembershipStatus : IMembersipStatus, IEntity
	{
		[Key]
        public int Id { get; set; }

		public int CustomerId { get; set; }
		public DateTime? VideoClubMembershiExpirationDate { get; set; }
		public DateTime? BookClubMembershiExpirationDate { get; set; }
		public DateTime? PremiumMembershiExpirationDate { get; set; }
	}
}