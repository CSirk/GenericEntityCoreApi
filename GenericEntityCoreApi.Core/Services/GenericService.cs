using AutoMapper;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.Core.Services.Interfaces;
using GenericEntityCoreApi.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericEntityCoreApi.Core.Services
{
    public abstract class GenericService<TDomain, TEntity, TContext> : IGenericService<TDomain, TEntity, TContext> 
        where TEntity: class where TContext: DbContext
    {
        private readonly IGenericRepository<TContext, TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(
            IGenericRepository<TContext, TEntity> repository,
            IMapper mapper
            )
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TDomain>> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var entityRecords = await this._repository.GetAll();
            return _mapper.Map<List<TEntity>, List<TDomain>>(entityRecords);
        }

        /// <summary>
        /// Get a single record
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TDomain> GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var entityRecord = await this._repository.GetSingle(predicate);
            return _mapper.Map<TDomain>(entityRecord);
        }

        /// <summary>
        /// Load multiple records without tracking
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<TDomain>> LoadMultiple(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var entityRecords = await _repository.LoadMultiple(predicate);
            return _mapper.Map<List<TEntity>, List<TDomain>>(entityRecords);
        }

        /// <summary>
        /// Load a single record without tracking
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TDomain> LoadSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var entityRecord = await _repository.LoadSingle(predicate);
            return _mapper.Map<TDomain>(entityRecord);
        }

        /// <summary>
        /// Looks up whether a record exists by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual async Task<bool> LookupSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var entityRecord = await LoadSingle(predicate, includes);
            return (entityRecord != null) ? true : false;
        }

        /// <summary>
        /// Return records matching predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<TDomain>> Search(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var entityRecords = await _repository.Search(predicate);
            return _mapper.Map<List<TEntity>, List<TDomain>>(entityRecords);
        }

        /// <summary>
        /// Add a record
        /// </summary>
        /// <param name="domainRecordToAdd"></param>
        /// <returns></returns>
        public async Task Add(TDomain domainRecordToAdd)
        {
            var entityToAdd = _mapper.Map<TEntity>(domainRecordToAdd);
            await _repository.Add(entityToAdd);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="domainRecordToUpdate"></param>
        /// <returns></returns>
        public async Task Update(TDomain domainRecordToUpdate)
        {
            var entityRecord = _mapper.Map<TEntity>(domainRecordToUpdate);
            await _repository.Update(entityRecord);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="domainRecordToDelete"></param>
        /// <returns></returns>
        public async Task Delete(TDomain domainRecordToDelete)
        {
            var entityRecord = _mapper.Map<TEntity>(domainRecordToDelete);
            await _repository.Delete(entityRecord);
        }
    }
}
