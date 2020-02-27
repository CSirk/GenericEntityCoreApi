using AutoMapper;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using GenericEntityCoreApi.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericEntityCoreApi.Core.Services
{
    public class AnimalService : GenericService<DomAnimal, Animal, csirk_ExampleDatabaseContext>
    {
        public AnimalService(IGenericRepository<csirk_ExampleDatabaseContext, Animal> repository,
            IMapper mapper) : base(repository, mapper) { }
    }
}
