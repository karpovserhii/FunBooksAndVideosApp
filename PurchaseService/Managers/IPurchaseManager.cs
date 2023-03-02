using Models;
using Models.Interfaces;
using System.Security.Cryptography;

namespace PurchaseService.Managers
{
	public interface IPurchaseManager
	{
		void RegisterPurchase(Purchase purchase);
		void CancelPurchase(IPurchase purchase);
		IPurchase GetPurchaseInfo(int id);
	}
}