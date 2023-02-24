using System.Collections.Generic;
using System.Linq.Expressions;

namespace Models.Interfaces
{
	public interface IPurchase : IEntity
	{
        Customer Customer { get; set; }
		int CustomerId { get; set; }
		double Total { get; set; }
	}
}