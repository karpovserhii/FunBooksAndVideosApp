using Common.Helpers;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
	public class BaseRepository<T> : AppDbContext, IRepository<T> where T : class, IEntity, new()
	{
		protected readonly ILogger<IRepository<T>> _logger;
		protected DbSet<T> _dbSet;

		public BaseRepository(DbContextOptions<AppDbContext> options, ILogger<IRepository<T>> logger):base(options)
		{
	
			_logger = logger;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync<T>();
		}
        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList<T>();
        }

        public  T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public T Create(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _dbSet.Entry(entity).State = EntityState.Added;
                SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add entity with Id: {entity.Id} to db for entity type: {typeof(T).Name}; Entity: {JsonHelper.ToJson(entity)}");
                return null;
            }

        }

        public async Task<T> CreateAsync(T entity)
		{
			try
			{
				_dbSet.Attach(entity);
				_dbSet.Entry(entity).State = EntityState.Added;
				await SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to add entity with Id: {entity.Id} to db for entity type: {typeof(T).Name}; Entity: {JsonHelper.ToJson(entity)}");
				return null;
			}

		}

		public void Update(T entity)
		{
			try
			{
				_dbSet.Attach(entity);
				Entry(entity).State = EntityState.Modified;
				SaveChanges();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to update entity with Id: {entity.Id} to db for entity type: {typeof(T).Name}; Entity: {JsonHelper.ToJson(entity)}");
			}

		}

        public bool Delete(T entity)
		{
			try
			{
				_dbSet.Remove(entity);
				SaveChanges();
				return true;
			}catch(Exception ex)
			{
				_logger.LogError(ex, $"Failed to delete entity with Id: {entity.Id} for entity: {typeof(T).Name}");
				return false;
			}
			
		}
	}
}
