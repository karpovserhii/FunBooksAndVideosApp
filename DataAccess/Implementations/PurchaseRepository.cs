using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;


namespace DataAccess.Implementations
{
	public class PurchaseRepository : BaseRepository<Purchase>
	{
		public PurchaseRepository(DbContextOptions<AppDbContext> options, ILogger<IRepository<Purchase>> logger) : base(options, logger)
		{
			_dbSet = Purchases;
		}
	}
}
