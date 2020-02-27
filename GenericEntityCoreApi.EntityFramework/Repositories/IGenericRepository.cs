using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericEntityCoreApi.EntityFramework.Repositories
{
    public interface IGenericRepository<C, T> where T : class where C : DbContext
    {
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<bool> LookupSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> LoadSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> LoadMultiple(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task Save();
    }
}
