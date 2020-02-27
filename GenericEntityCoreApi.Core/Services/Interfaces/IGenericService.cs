using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericEntityCoreApi.Core.Services.Interfaces
{
    public interface IGenericService<TDomain, TEntity, TContext>
        where TEntity : class where TContext : DbContext
    {
        Task<List<TDomain>> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<TDomain> GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TDomain>> LoadMultiple(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TDomain> LoadSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> LookupSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TDomain>> Search(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task Add(TDomain domainRecordToAdd);
        Task Update(TDomain domainRecordToUpdate);
        Task Delete(TDomain domainRecordToDelete);
    }
}
