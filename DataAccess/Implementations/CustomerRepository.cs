using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;


namespace DataAccess.Implementations
{
	public class CustomerRepository : BaseRepository<Customer>
	{
		public CustomerRepository(DbContextOptions<AppDbContext> options, ILogger<BaseRepository<Customer>> logger) : base(options, logger)
		{
			_dbSet = Customers;
		}
	}
}
