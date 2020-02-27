using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericEntityCoreApi.EntityFramework.Repositories
{
    public class AnimalRepository : GenericRepository<csirk_ExampleDatabaseContext, Animal>
    {
        public AnimalRepository(csirk_ExampleDatabaseContext context) : base(context) { }
    }
}
