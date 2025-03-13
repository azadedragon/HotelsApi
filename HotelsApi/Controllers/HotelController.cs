using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("Hotel")]

    public class HotelController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public HotelController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet()]
        public async Task<List<Hotel>> GetAllHotels()
        {
            var Hotel = await databaseContext.Hotels.ToListAsync();

            return Hotel;
        }

        [HttpGet("{id}")]
        public async Task<Hotel?> GetHotelById([FromRoute] int id)
        {
            var Hotel = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);

            return Hotel;
        }

        [HttpPost()]
        public async Task<Hotel> CreateHotel([FromBody] Hotel hotel)
        {
            databaseContext.Hotels.Add(hotel);
            await databaseContext.SaveChangesAsync();

            return hotel;
        }

        [HttpPut("{id}")]
        public async Task<Hotel?> UpdateHotel([FromRoute] int id, [FromBody] Hotel hotel)
        {
           
            var hotelRecord = await databaseContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);

            if (hotelRecord == null)
            {
                return null;
            }

            hotelRecord.HotelCode = hotel.HotelCode;
            hotelRecord.HotelName = hotel.HotelName;
            hotelRecord.HotelDescription = hotel.HotelDescription;

            await databaseContext.SaveChangesAsync();

            return hotelRecord;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteHotel([FromRoute] int id)
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
    }
}


