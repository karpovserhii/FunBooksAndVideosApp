using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;
using Models.Interfaces.Api;
using Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseService.Managers
{
	public class PurchaseManager
	{
		private readonly PurchaseRepository _repo;
		private readonly PurchaseProcessorFactory factory;
        public PurchaseManager(IRepository<Purchase> repository, IMembershipActivationService membershipService, IShippingService shippingService)
		{
			_repo = repository as PurchaseRepository;
			factory = new PurchaseProcessorFactory(membershipService, shippingService);
		}
		[HttpPost]
		public async Task RegisterPurchaseAsync(Purchase purchase)
		{
			var processor = factory.CreatePurchaseOrderProcessor(purchase);
			var result = await processor.ProcessPurchaseAsync(purchase);
			if (!result.Success)
			{
				return;
			}
			_repo.Create(purchase);
			_repo.SaveChanges();
		}
		[HttpDelete]
		public void DeletePurchase(IPurchase purchase)
		{
			_repo.Delete(purchase as Purchase);
            _repo.SaveChanges();
        }

		public IPurchase GetPurchaseInfo(int id)
		{
			return _repo.GetById(id);
		}
	}
}
