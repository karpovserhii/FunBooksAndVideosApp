using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Implementations.Processors;
using Models.Interfaces;
using Models.Interfaces.Api;
using Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseService.Managers
{
	public class PurchaseManager : IPurchaseManager
	{
		private readonly PurchaseRepository _repo;
		private readonly PurchaseProcessor processor;
        public PurchaseManager(IRepository<Purchase> repository, IMembershipActivationService membershipService, IShippingService shippingService)
		{
			_repo = repository as PurchaseRepository;
			processor = new PurchaseProcessor(membershipService, shippingService);
		}
		[HttpPost]
		public void RegisterPurchase(Purchase purchase)
		{
			var result = processor.ProcessPurchase(purchase);
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

		public void CancelPurchase(IPurchase purchase)
		{
			_repo.Delete(purchase as Purchase);
		}
	}
}
