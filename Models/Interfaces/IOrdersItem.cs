
using Models.Enums;

namespace Models.Interfaces
{
	public interface IOrdersItem : IEntity
	{
		OrderItemType OrderItemType { get; set; }
		string NameOfItem { get; set; } 
		float Price { get; set; }
	}
}