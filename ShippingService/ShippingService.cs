using DataAccess.Implementations;
using DataAccess.Interfaces;
using Models;
using Models.Interfaces;
using Models.Interfaces.Services;

namespace ShippingService
{
	public class ShippingService : IShippingService
	{
		private readonly CustomerRepository _repo;
		private readonly OrderItemsRepository _orderItemsRepository;
        public ShippingService(IRepository<Customer> repository, IRepository<OrderItem> orderItemsRepository)
        {
			_repo = repository as CustomerRepository;
			_orderItemsRepository = orderItemsRepository as OrderItemsRepository;
        }
        public ShippingSlip GenerateShippingSlip(int customeId, OrderPosition item)
		{
			var customer = _repo.GetById(customeId);
			var shippingItem = _orderItemsRepository.GetById(item.OrderItemId);
			if(customer == null || shippingItem == null) { return null; }
            return new ShippingSlip()
			{
				Customer = customer,
				ShippingItem = shippingItem
            };
		}
	}
}
