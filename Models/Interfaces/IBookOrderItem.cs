using System;


namespace Models.Interfaces
{
	public interface IBookOrderItem : IRealProductOrderItem
	{
		string Author { get; set; }
		string Title { get; set; }
		DateTime? PublicationDate { get; set; }

	}
}
