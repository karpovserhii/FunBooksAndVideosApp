using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;


namespace DataAccess.Implementations
{
	public class OrderItemsRepository : BaseRepository<OrderItem>
	{
		public OrderItemsRepository(DbContextOptions<AppDbContext> options, ILogger<BaseRepository<OrderItem>> logger) : base(options, logger)
		{
			_dbSet = OrderItems;
		}
	}
}
