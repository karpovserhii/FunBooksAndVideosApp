using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces.Api
{
	public interface ICustomerApi
	{
        [Get("/customers")]
        Task<List<Customer>> GetAllCustomerss();
        [Get("/cutomers/{customerId}")]
        Task<Customer> GetCustomerById(int customerId);
        [Post("/customers/{customerId}")]
        Task CreateCreateCustomer(int customerId, IOrder order);

    }
}
