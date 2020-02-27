using AutoMapper;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using GenericEntityCoreApi.EntityFramework.Repositories;

namespace GenericEntityCoreApi.Core.Services
{
    public class CustomerService : GenericService<DomCustomer, Customer, csirk_ExampleDatabaseContext>
    {
        public CustomerService(
            IGenericRepository<csirk_ExampleDatabaseContext, Customer> repository,
            IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
