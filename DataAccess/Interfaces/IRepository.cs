using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IRepository<T> where T: IEntity
	{
		Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll();
		T GetById(int id);
		Task<T> GetByIdAsync(int id);
		void Update(T souce);
		bool Delete(T source);
		Task<T> CreateAsync(T source);
        T Create(T source);
    }
}
