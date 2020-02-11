using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericEntityCoreApi.EntityFramework.Repositories
{
    public class CustomerRepository : GenericRepository<csirk_ExampleDatabaseContext, Customer>
    {
        public CustomerRepository(csirk_ExampleDatabaseContext context) : base(context) { }
    }
}
