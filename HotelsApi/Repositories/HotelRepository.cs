using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{

    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllHotels();
        Task<Hotel?> GetHotelById(int id);
        Task<Hotel> CreateHotel(Hotel hotel);
        Task<Hotel?> UpdateHotel(int id, Hotel hotel);
        Task<bool> DeleteHotel(int id);
        Task<bool> HotelCodeExists(string hotelCode, CancellationToken cancellationToken = default);
        Task<bool> HotelNameExists(string hotelName, CancellationToken cancellationToken = default);
        Task<bool> HotelIdExists(int hotelid, CancellationToken cancellation = default);
    }

    public class HotelRepository : IHotelRepository
    {
        private readonly DatabaseContext databaseContext;
        public HotelRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Hotel>> GetAllHotels()
        {
            var Hotel = await databaseContext.Hotels.ToListAsync();
            return Hotel;
        }
        public async Task<Hotel?> GetHotelById(int id)
        {
            var Hotel = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            return Hotel;
        }
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            databaseContext.Hotels.Add(hotel);
            await databaseContext.SaveChangesAsync();
            return hotel;
        }
        public async Task<Hotel?> UpdateHotel(int id, Hotel hotel)
        {
            var hotelRecord = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            if (hotelRecord == null)
            {
                return null;
            }
            hotelRecord.HotelCode = hotel.HotelCode;
            hotelRecord.HotelName = hotel.HotelName;
            hotelRecord.HotelDescription = hotel.HotelDescription;
            hotelRecord.BarangayId = hotel.BarangayId;
            await databaseContext.SaveChangesAsync();
            return hotelRecord;
        }
        public async Task<bool> DeleteHotel(int id)
        {
            var hotelRecord = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
            if (hotelRecord == null)
            {
                return false;
            }
            databaseContext.Hotels.Remove(hotelRecord);
            await databaseContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> HotelIdExists(int hotelId, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Hotels
                .AnyAsync(h => h.HotelId == hotelId, cancellationToken);
        }
        public async Task<bool> HotelCodeExists(string hotelCode, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Hotels
                .AnyAsync(h => h.HotelCode == hotelCode, cancellationToken);
        }
        public async Task<bool> HotelNameExists(string hotelName, CancellationToken cancellationToken = default)
        {
            return await databaseContext.Hotels
                .AnyAsync(h => h.HotelName == hotelName, cancellationToken);
        }
    }

}
    
    

