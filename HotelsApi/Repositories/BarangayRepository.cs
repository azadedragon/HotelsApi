using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface IBarangayRepository
    {
        Task<List<Barangay>> GetAllBarangay();
        Task<Barangay?> GetBarangayById(int id);
        Task<Barangay> CreateBarangay(Barangay barangay);
        Task<Barangay?> UpdateBarangay(int id, Barangay barangay);
        Task<bool> DeleteBarangay(int id);

        Task<bool> PostalCodeExists(string postalCode, CancellationToken cancellationToken = default);
        Task<bool> BarangayNameExists(string barangayName, CancellationToken cancellationToken = default);
        Task<bool> BarangayExistsAsync(int barangayId, CancellationToken cancellationToken = default);

    }
    public class BarangayRepository : IBarangayRepository
    {
        private readonly DatabaseContext databaseContext;
        public BarangayRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Barangay>> GetAllBarangay()
        {
            var Barangay = await databaseContext.Barangays.ToListAsync();
            return Barangay;
        }
        public async Task<Barangay?> GetBarangayById(int id)
        {
            var Barangay = await databaseContext.Barangays.FirstOrDefaultAsync(x => x.BarangayId == id);
            return Barangay;
        }
        public async Task<Barangay> CreateBarangay(Barangay barangay)
        {
            databaseContext.Barangays.Add(barangay);
            await databaseContext.SaveChangesAsync();
            return barangay;
        }
        public async Task<Barangay?> UpdateBarangay(int id, Barangay barangay)
        {
            var barangayRecord = await databaseContext.Barangays.FirstOrDefaultAsync(x => x.BarangayId == id);
            if (barangayRecord == null)
            {
                return null;
            }
            barangayRecord.PostalCode = barangay.PostalCode;
            barangayRecord.BarangayName = barangay.BarangayName;
            barangayRecord.CityId = barangay.CityId;
            await databaseContext.SaveChangesAsync();
            return barangayRecord;
        }
        public async Task<bool> DeleteBarangay(int id)
        {
            var barangayRecord = await databaseContext.Barangays.FirstOrDefaultAsync(x => x.BarangayId == id);
            if (barangayRecord == null)
            {
                return false;
            }
            databaseContext.Barangays.Remove(barangayRecord);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostalCodeExists(string postalCode, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Barangays
                .AnyAsync(h => h.PostalCode == postalCode, cancellationToken);
        }
        public async Task<bool> BarangayNameExists(string barangayName, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Barangays
                .AnyAsync(h => h.BarangayName == barangayName, cancellationToken);
        }
        public async Task<bool> BarangayExistsAsync(int barangayId, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Barangays.AnyAsync(s => s.BarangayId == barangayId, cancellationToken);
        }

    }

}
