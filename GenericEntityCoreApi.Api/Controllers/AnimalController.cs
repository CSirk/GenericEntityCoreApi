using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.Core.Services.Interfaces;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericEntityCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IGenericService<DomAnimal, Animal, csirk_ExampleDatabaseContext> _animalService;

        public AnimalController(
            IGenericService<DomAnimal, Animal, csirk_ExampleDatabaseContext> animalService)
        {
            this._animalService = animalService;
        }

        [HttpGet("GetAllAnimals")]
        public async Task<List<DomAnimal>> GetAnimals()
        {
            return await _animalService.GetAll();
        }

        [HttpGet("GetSingleAnimal")]
        public async Task<DomAnimal> GetSingleAnimal([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch (searchType)
            {
                case "Name":
                    return await _animalService.GetSingle(x => x.Name == searchTerm);
                case "Nickname":
                    return await _animalService.GetSingle(x => x.Nickname == searchTerm);
                case "Legs":
                    return await _animalService.GetSingle(x => x.NumberOfLegs == Convert.ToInt32(searchTerm));
                case "IsPet":
                    return await _animalService.GetSingle(x => x.IsPet == 1);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpGet("SearchAnimals")]
        public async Task<List<DomAnimal>> SearchAnimals([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch (searchType)
            {
                case "Name":
                    return await _animalService.Search(x => x.Name == searchTerm);
                case "NickName":
                    return await _animalService.Search(x => x.Nickname == searchTerm);
                case "Legs":
                    return await _animalService.Search(x => x.NumberOfLegs == Convert.ToInt32(searchTerm));
                case "IsPet":
                    return await _animalService.Search(x => x.IsPet == 1);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpGet("LookUpSingleAnimal")]
        public async Task<bool> LookUpAnimal([FromQuery] string searchType, [FromQuery] string searchTerm)
        {
            switch (searchType)
            {
                case "Name":
                    return await _animalService.LookupSingle(x => x.Name == searchTerm);
                case "Nickname":
                    return await _animalService.LookupSingle(x => x.Nickname == searchTerm);
                case "Legs":
                    return await _animalService.LookupSingle(x => x.NumberOfLegs == Convert.ToInt32(searchTerm));
                case "IsPet":
                    return await _animalService.LookupSingle(x => x.IsPet == 1);
                default:
                    throw new Exception("Search Type not supported.");
            }
        }

        [HttpPost("AddAnimal")]
        public async Task AddAnimal([FromBody] DomAnimal animalToAdd)
        {
            await _animalService.Add(animalToAdd);
        }

        [HttpPost("UpdateAnimal")]
        public async Task UpdateAnimal([FromBody] DomAnimal animalToUpdate)
        {
            await _animalService.Update(animalToUpdate);
        }

        [HttpDelete("DeleteAnimal")]
        public async Task DeleteAnimal([FromBody] DomAnimal animalToDelete)
        {
            await _animalService.Delete(animalToDelete);
        }
    }
}