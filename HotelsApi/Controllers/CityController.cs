using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("City")]

    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCity()
        {
            var city = await cityService.GetAllCity();

            return Ok(city);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById([FromRoute] int id)
        {
            var city = await cityService.GetCityById(id);
            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCity([FromBody] CreateCity city)
        {
            var createdCityd = await cityService.CreateCity(city);

            if (createdCityd == null)
                return BadRequest();

            return Ok(createdCityd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity([FromRoute] int id, [FromBody] UpdateCity city)
        {
            city.CityId = id;
            var updateCityResult = await cityService.UpdateCity(id, city);

            if (updateCityResult == null)
                return BadRequest();

            return Ok(updateCityResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            var deleteResult = await cityService.DeleteCity(id);
            if (deleteResult == false)
                return BadRequest();

            return Ok(deleteResult);
        }
    }
}


