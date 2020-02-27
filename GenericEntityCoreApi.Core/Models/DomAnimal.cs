using System;
using System.Collections.Generic;
using System.Text;

namespace GenericEntityCoreApi.Core.Models
{
    public class DomAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int NumberOfLegs { get; set; }
        public bool IsPet { get; set; }
    }
}
