using GenericEntityCoreApi.Core.Extensions;
using GenericEntityCoreApi.Core.Interfaces;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using GenericEntityCoreApi.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericEntityCoreApi.Core
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<csirk_ExampleDatabaseContext, Customer> _customerRepository;

        public CustomerService(
            IGenericRepository<csirk_ExampleDatabaseContext, Customer> customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task AddCustomer(DomCustomer customerToAdd)
        {
            var entityRecord = customerToAdd.ToEntityCustomer();
            await _customerRepository.Add(entityRecord);
        }

        public async Task DeleteCustomer(DomCustomer customerToDelete)
        {
            var entityRecord = customerToDelete.ToEntityCustomer();
            await _customerRepository.Delete(entityRecord);
        }

        public async Task<List<DomCustomer>> GetAllCustomers()
        {
            var entityCustomers = await _customerRepository.GetAll();
            return entityCustomers.ToDomainCustomerList();
        }

        public async Task<List<DomCustomer>> SearchCustomers(Expression<Func<Customer, bool>> predicate)
        {
            var entityRecords = await _customerRepository.FindBy(predicate);
            return entityRecords.ToDomainCustomerList();
        }

        public async Task UpdateCustomer(DomCustomer customerToUpdate)
        {
            var entityRecord = customerToUpdate.ToEntityCustomer();
            await _customerRepository.Update(entityRecord);
        }
    }
}
