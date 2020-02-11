using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericEntityCoreApi.EntityFramework.Repositories
{
    public abstract class GenericRepository<C, T> :
        IGenericRepository<C, T> where T : class where C : DbContext
    {
        private C _context { get; set; }

        public GenericRepository(DbContext context)
        {
            _context = context as C;
        }

        public virtual async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            if (includes.Length > 0)
            {
                var queryable = _context.Set<T>().AsQueryable();
                var result = await includes.Aggregate(queryable, (current, include) => current.Include(include)).ToListAsync();
                return result;
            }

            var records = await _context.Set<T>().ToListAsync();
            
            return records;
        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes.Length > 0)
            {
                var queryable = _context.Set<T>().AsQueryable();
                var result = await includes.Aggregate(queryable, (current, include) => current.Include(include)).Where(predicate).FirstOrDefaultAsync();
                return result;
            }

            var record = await _context.Set<T>().FirstOrDefaultAsync(predicate);

            return record;
        }

        public virtual async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes.Length > 0)
            {
                var queryable = _context.Set<T>().AsQueryable();
                var result = await includes.Aggregate(queryable, (current, include) => current.Include(include)).Where(predicate).ToListAsync();

                return result;
            }

            var results = await _context.Set<T>().Where(predicate).ToListAsync();

            return results;
        }

        public virtual async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await Save();

            return entity;
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Save();
        }

        public virtual async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await Save();
        }

        public virtual async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
