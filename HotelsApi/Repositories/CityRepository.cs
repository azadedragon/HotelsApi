using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllCity();
        Task<City?> GetCityById(int id);
        Task<City> CreateCity(City city);
        Task<City?> UpdateCity(int id, City city);
        Task<bool> DeleteCity(int id);
        Task<bool> CityCodeExists(string cityCode, CancellationToken cancellationToken = default);
        Task<bool> CityNameExists(string cityName, CancellationToken cancellationToken = default);
        Task<bool> CityExistsAsync(int cityId, CancellationToken cancellationToken = default);
    }
    public class CityRepository : ICityRepository
    {
        private readonly DatabaseContext databaseContext;
        public CityRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<City>> GetAllCity()
        {
            var City = await databaseContext.Cities.ToListAsync();
            return City;
        }
        public async Task<City?> GetCityById(int id)
        {
            var City = await databaseContext.Cities.FirstOrDefaultAsync(x => x.CityId == id);
            return City;
        }
        public async Task<City> CreateCity(City city)
        {
            databaseContext.Cities.Add(city);
            await databaseContext.SaveChangesAsync();
            return city;
        }
        public async Task<City?> UpdateCity(int id, City city)
        {
            var cityRecord = await databaseContext.Cities.FirstOrDefaultAsync(x => x.CityId == id);
            if (cityRecord == null)
            {
                return null;
            }
            cityRecord.CityCode = city.CityCode;
            cityRecord.CityName = city.CityName;
            cityRecord.StateId = city.StateId;
            await databaseContext.SaveChangesAsync();
            return cityRecord;
        }
        public async Task<bool> DeleteCity(int id)
        {
            var cityRecord = await databaseContext.Cities.FirstOrDefaultAsync(x => x.CityId == id);
            if (cityRecord == null)
            {
                return false;
            }
            databaseContext.Cities.Remove(cityRecord);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CityCodeExists(string cityCode, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Cities
                .AnyAsync(h => h.CityCode == cityCode, cancellationToken);
        }
        public async Task<bool> CityNameExists(string cityName, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Cities
                .AnyAsync(h => h.CityName == cityName, cancellationToken);
        }
        public async Task<bool> CityExistsAsync(int cityId, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Cities.AnyAsync(s => s.CityId == cityId, cancellationToken);
        }
    }
}
