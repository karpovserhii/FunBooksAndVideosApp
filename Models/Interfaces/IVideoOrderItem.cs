
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Interfaces
{
	public interface IVideoOrderItem : IRealProductOrderItem
	{
		string Caption { get; set; }
		string Author { get; set; }
		TimeSpan? Length { get; set; }
	}
}
