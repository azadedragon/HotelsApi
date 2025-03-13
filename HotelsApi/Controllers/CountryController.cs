using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("Country")]

    public class CountryController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public CountryController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet()]
        public async Task<List<Country>> GetAllCountry()
        {
            var Country = await databaseContext.Countries.ToListAsync();

            return Country;
        }

        [HttpGet("{id}")]
        public async Task<Country?> GetCountryById([FromRoute] int id)
        {
            var Country = await databaseContext.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

            return Country;
        }

        [HttpPost()]
        public async Task<Country> CreateCountry([FromBody] Country country)
        {
            databaseContext.Countries.Add(country);
            await databaseContext.SaveChangesAsync();

            return country;
        }

        [HttpPut("{id}")]
        public async Task<Country?> UpdateCountry([FromRoute] int id, [FromBody] Country country)
        {

            var countryRecord = await databaseContext.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

            if (countryRecord == null)
            {
                return null;
            }

            countryRecord.CountryCode = country.CountryCode;
            countryRecord.CountryName = country.CountryName;
         
            await databaseContext.SaveChangesAsync();

            return countryRecord;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCountry([FromRoute] int id)
        {
            var countryRecord = await databaseContext.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

            if (countryRecord == null)
            {
                return false;
            }

            databaseContext.Countries.Remove(countryRecord);

            await databaseContext.SaveChangesAsync();

            return true;

        }
    }
}


