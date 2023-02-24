using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;
using Models.Interfaces.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseService.Managers
{
	public class PurchaseManager
	{
		private readonly PurchaseRepository _repo;
		private readonly IMembershipApi _membershipApi;
		public PurchaseManager(IRepository<Purchase> repository, IMembershipApi membershipApi)
		{
			_repo = repository as PurchaseRepository;
			_membershipApi = membershipApi;
		}
		[HttpPost]
		public async Task RegisterPurchaseAsync(Purchase purchase)
		{
			if(purchase.Orders.Any(t => t.OrderItem.OrderItemType != Models.Enums.OrderItemType.Book || t.OrderItem.OrderItemType != Models.Enums.OrderItemType.Video) == true)
			{
				await _membershipApi.CreateMembershipByOrder(purchase.CustomerId, purchase.Orders.Select(t => t.OrderItem).ToList());
			}
			_repo.Create(purchase as Purchase);
		}
		[HttpDelete]
		public void CancelPurchase(IPurchase purchase)
		{
			_repo.Delete(purchase as Purchase);
		}

		public  IPurchase GetPurchaseInfo(int id)
		{
			return  _repo.GetById(id);
		}
	}
}
