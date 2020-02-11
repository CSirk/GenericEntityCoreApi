using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericEntityCoreApi.Core.Interfaces;
using GenericEntityCoreApi.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericEntityCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<List<DomCustomer>> GetCustomers()
        {
            return await _customerService.GetAllCustomers();
        }

        [HttpGet("SearchCustomers")]
        public async Task<List<DomCustomer>> SearchCustomers([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch(searchType)
            {
                case "FirstName":
                    return await _customerService.SearchCustomers(x => x.FirstName == searchTerm);
                case "LastName":
                    return await _customerService.SearchCustomers(x => x.LastName == searchTerm);
                case "Age":
                    return await _customerService.SearchCustomers(x => x.Age == Convert.ToInt32(searchTerm));
                case "Gender":
                    return await _customerService.SearchCustomers(x => x.Gender == searchTerm);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpPost("AddCustomer")]
        public async Task AddCustomer([FromBody] DomCustomer customerToAdd)
        {
            await _customerService.AddCustomer(customerToAdd);
        }

        [HttpPost("UpdateCustomer")]
        public async Task UpdateCustomer([FromBody] DomCustomer customerToUpdate)
        {
            await _customerService.UpdateCustomer(customerToUpdate);
        }

        [HttpDelete("DeleteCustomer")]
        public async Task DeleteCustomer([FromBody] DomCustomer customerToDelete)
        {
            await _customerService.DeleteCustomer(customerToDelete);
        }
    }
}