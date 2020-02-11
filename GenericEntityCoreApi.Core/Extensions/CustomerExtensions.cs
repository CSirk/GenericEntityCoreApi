using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericEntityCoreApi.Core.Extensions
{
    public static class CustomerExtensions
    {
        public static Customer ToEntityCustomer(this DomCustomer domCustomer)
        {
            return new Customer
            {
                Id = domCustomer.Id,
                FirstName = domCustomer.FirstName,
                LastName = domCustomer.LastName,
                Gender = domCustomer.Gender,
                Age = domCustomer.Age
            };
        }

        public static DomCustomer ToDomainCustomer(this Customer entityCustomer)
        {
            return new DomCustomer
            {
                Id = entityCustomer.Id,
                FirstName = entityCustomer.FirstName,
                LastName = entityCustomer.LastName,
                Gender = entityCustomer.Gender,
                Age = entityCustomer.Age
            };
        }

        public static List<DomCustomer> ToDomainCustomerList(this List<Customer> entityCustomers)
        {
            var listOfDomCustomers = new List<DomCustomer>();

            entityCustomers.ForEach(x => listOfDomCustomers.Add(x.ToDomainCustomer()));

            return listOfDomCustomers;
        }
    }
}
