using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.Core.Services;
using GenericEntityCoreApi.Core.Services.Interfaces;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericEntityCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericService<DomCustomer, Customer, csirk_ExampleDatabaseContext> _customerService;

        public CustomerController(
            IGenericService<DomCustomer, Customer, csirk_ExampleDatabaseContext> customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<List<DomCustomer>> GetCustomers()
        {
            return await _customerService.GetAll();
        }

        [HttpGet("GetSingleCustomer")]
        public async Task<DomCustomer> GetSingleCustomer([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch (searchType)
            {
                case "FirstName":
                    return await _customerService.GetSingle(x => x.FirstName == searchTerm);
                case "LastName":
                    return await _customerService.GetSingle(x => x.LastName == searchTerm);
                case "Age":
                    return await _customerService.GetSingle(x => x.Age == Convert.ToInt32(searchTerm));
                case "Gender":
                    return await _customerService.GetSingle(x => x.Gender == searchTerm);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpGet("SearchCustomers")]
        public async Task<List<DomCustomer>> SearchCustomers([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch(searchType)
            {
                case "FirstName":
                    return await _customerService.Search(x => x.FirstName == searchTerm);
                case "LastName":
                    return await _customerService.Search(x => x.LastName == searchTerm);
                case "Age":
                    return await _customerService.Search(x => x.Age == Convert.ToInt32(searchTerm));
                case "Gender":
                    return await _customerService.Search(x => x.Gender == searchTerm);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpGet("LookUpSingleCustomer")]
        public async Task<bool> LookUpCustomer([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch (searchType)
            {
                case "FirstName":
                    return await _customerService.LookupSingle(x => x.FirstName == searchTerm);
                case "LastName":
                    return await _customerService.LookupSingle(x => x.LastName == searchTerm);
                case "Age":
                    return await _customerService.LookupSingle(x => x.Age == Convert.ToInt32(searchTerm));
                case "Gender":
                    return await _customerService.LookupSingle(x => x.Gender == searchTerm);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpPost("AddCustomer")]
        public async Task AddCustomer([FromBody] DomCustomer customerToAdd)
        {
            await _customerService.Add(customerToAdd);
        }

        [HttpPost("UpdateCustomer")]
        public async Task UpdateCustomer([FromBody] DomCustomer customerToUpdate)
        {
            await _customerService.Update(customerToUpdate);
        }

        [HttpDelete("DeleteCustomer")]
        public async Task DeleteCustomer([FromBody] DomCustomer customerToDelete)
        {
            await _customerService.Delete(customerToDelete);
        }
    }
}