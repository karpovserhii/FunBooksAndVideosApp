using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using Models.Interfaces.Api;
using Models.Interfaces.Services;
using PurchaseService.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
	{

		private readonly ILogger<PurchaseController> _logger;
		private readonly PurchaseManager _manager;


		public PurchaseController(ILogger<PurchaseController> logger, IRepository<Purchase> repository, IMembershipActivationService membershipActivationService, IShippingService shippingService)
		{
			_logger = logger;
			_manager = new PurchaseManager(repository, membershipActivationService, shippingService);
		}

		[HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPurchaseById(int id)
		{
			var purchase = _manager.GetPurchaseInfo(id);
			if (purchase == null)
			{
				return NotFound();
			}

			return Ok(purchase);

		}
		[HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RegisterPurchase(Purchase purchase)
		{
			_manager.RegisterPurchase(purchase);
			return Ok();
		}
	}
}
