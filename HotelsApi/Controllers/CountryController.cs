using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("Country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCountry()
        {
            var countrys = await countryService.GetAllCountry();
            return Ok(countrys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById([FromRoute] int id)
        {
            var country = await countryService.GetCountryById(id);
            if (country == null)
                return NotFound();

            return Ok(country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry([FromRoute] int id, [FromBody] UpdateCountry country)
        {
            var updateCountryResult = await countryService.UpdateCountry(id, country);

            if (updateCountryResult == null)
                return BadRequest();

            return Ok(updateCountryResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        {
            var deleteResult = await countryService.DeleteCountry(id);
            if (deleteResult == false)
                return BadRequest();

            return Ok(deleteResult);
        }
    }
}


