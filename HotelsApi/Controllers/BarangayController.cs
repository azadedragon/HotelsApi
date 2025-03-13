using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("Barangay")]

    public class BarangayController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public BarangayController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet()]
        public async Task<List<Barangay>> GetAllBarangays()
        {
            var Barangay = await databaseContext.Barangays.ToListAsync();

            return Barangay;
        }

        [HttpGet("{id}")]
        public async Task<Barangay?> GetBarangayById([FromRoute] int id)
        {
            var Barangay = await databaseContext.Barangays.FirstOrDefaultAsync(x => x.BarangayId == id);

            return Barangay;
        }

        [HttpPost()]
        public async Task<Barangay> CreateBarangay([FromBody] Barangay barangay)
        {
            databaseContext.Barangays.Add(barangay);
            await databaseContext.SaveChangesAsync();

            return barangay;
        }

        [HttpPut("{id}")]
        public async Task<Barangay?> UpdateBarangay([FromRoute] int id, [FromBody] Barangay barangay)
        {

            var barangayRecord = await databaseContext.Barangays.FirstOrDefaultAsync(x => x.BarangayId == id);

            if (barangayRecord == null)
            {
                return null;
            }

            barangayRecord.PostalCode = barangay.PostalCode;
            barangayRecord.BarangayName = barangay.BarangayName;

            await databaseContext.SaveChangesAsync();

            return barangayRecord;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteBarangay([FromRoute] int id)
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
    }
}



