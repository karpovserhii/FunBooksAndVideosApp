using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataAccess.Implementations;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MembershipService.Managers;
using Models.Interfaces;

namespace MembershipService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MembershipController : ControllerBase
	{
		private readonly ILogger<MembershipController> _logger;
        private MembershipManager _manager;

		public MembershipController(ILogger<MembershipController> logger, IRepository<MembershipStatus> membershipRepository)
		{
			_logger = logger;
            _manager = new MembershipManager(membershipRepository as MembershipRepository);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMemberships()
        {
            return Ok(await _manager.GetMembershipStatuses());
        }

        [HttpGet("{customerId}"), ActionName("GetCustomerMembership")]
        public IActionResult GetMembershipByCustomerId(int customerId)
        {
            var membershipStatus = _manager.GetMembershipForCustomer(customerId);
            if (membershipStatus == null)
            {
                return NotFound();
            }

            return Ok(membershipStatus);
        }

        [HttpPost(), ActionName("CreateCustomerMembership")]
        public IActionResult CreateMembershipByPurchase(Purchase purchase)
        {
            _manager.UpdateStatus(purchase.CustomerId, purchase);
            var currentStatus = GetMembershipByCustomerId(purchase.CustomerId);
            return CreatedAtAction(nameof(GetMembershipByCustomerId), new { id = purchase.CustomerId }, currentStatus);
        }

        [HttpPut]
        public IActionResult UpdateMembershipForCustomer(MembershipStatus status)
        {
             _manager.AddOrUpdateMembershipForCustomer(status);

            return NoContent();
        }
    }
}
