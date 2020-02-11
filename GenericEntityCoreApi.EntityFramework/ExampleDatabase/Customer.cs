using System;
using System.Collections.Generic;

namespace GenericEntityCoreApi.EntityFramework.ExampleDatabase
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
