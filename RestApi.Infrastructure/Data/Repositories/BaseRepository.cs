using Microsoft.EntityFrameworkCore;
using RestApi.Core.Entities;
using RestApi.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace RestApi.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async virtual Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async virtual Task<T> Insert(T entity)
        {
            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<T> Update(T entity)
        {
            var fromDb = await this.GetById(entity.Id);
            if (fromDb == null)
                return null;

            _dbContext.Entry(fromDb).CurrentValues.SetValues(entity);
            _dbContext.Update(fromDb);

            await _dbContext.SaveChangesAsync();
            return fromDb;
        }

        public async virtual Task Delete(int id)
        {
            T entity = await _entities.FindAsync(id);
            if (entity != null)
            {
                _entities.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
