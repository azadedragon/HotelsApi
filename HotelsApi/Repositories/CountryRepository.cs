using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountry();
        Task<Country?> GetCountryById(int id);
        Task<Country> CreateCountry(Country country);
        Task<Country?> UpdateCountry(int id, Country country);
        Task<bool> DeleteCountry(int id);
        Task<bool> CountryCodeExists(string countryCode, CancellationToken cancellationToken = default);
        Task<bool> CountryNameExists(string countryName, CancellationToken cancellationToken = default);
        Task<bool> CountryExistsAsync(int countryId, CancellationToken cancellationToken = default);
    }
    public class CountryRepository : ICountryRepository
    {
        private readonly DatabaseContext databaseContext;
        public CountryRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Country>> GetAllCountry()
        {
            var Country = await databaseContext.Countries.ToListAsync();
            return Country;
        }
        public async Task<Country?> GetCountryById(int id)
        {
            var Country = await databaseContext.Countries.FirstOrDefaultAsync(x => x.CountryId == id);
            return Country;
        }
        public async Task<Country> CreateCountry(Country country)
        {
            databaseContext.Countries.Add(country);
            await databaseContext.SaveChangesAsync();
            return country;
        }
        public async Task<Country?> UpdateCountry(int id, Country country)
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
        public async Task<bool> DeleteCountry(int id)
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
        public async Task<bool> CountryCodeExists(string countryCode, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Countries
                .AnyAsync(h => h.CountryCode == countryCode, cancellationToken);
        }
        public async Task<bool> CountryNameExists(string countryName, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Countries
                .AnyAsync(h => h.CountryName == countryName, cancellationToken);
        }
        public async Task<bool> CountryExistsAsync(int countryId, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Countries.AnyAsync(s => s.CountryId == countryId, cancellationToken);
        }
    }
}
