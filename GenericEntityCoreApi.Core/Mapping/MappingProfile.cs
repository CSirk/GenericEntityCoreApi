using AutoMapper;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericEntityCoreApi.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, DomCustomer>().ReverseMap();
        }
    }
}
