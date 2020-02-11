using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericEntityCoreApi.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<List<DomCustomer>> GetAllCustomers();
        Task<List<DomCustomer>> SearchCustomers(Expression<Func<Customer, bool>> predicate);
        Task AddCustomer(DomCustomer customerToAdd);
        Task UpdateCustomer(DomCustomer customerToUpdate);
        Task DeleteCustomer(DomCustomer customerToDelete);
    }
}
