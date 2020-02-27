using System;
using System.Collections.Generic;

namespace GenericEntityCoreApi.EntityFramework.ExampleDatabase
{
    public partial class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int NumberOfLegs { get; set; }
        public byte IsPet { get; set; }
    }
}
