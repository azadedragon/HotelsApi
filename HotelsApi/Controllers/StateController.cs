using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("State")]

    public class StateController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public StateController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet()]
        public async Task<List<State>> GetAllStates()
        {
            var State = await databaseContext.States.ToListAsync();

            return State;
        }

        [HttpGet("{id}")]
        public async Task<State?> GetStateById([FromRoute] int id)
        {
            var State = await databaseContext.States.FirstOrDefaultAsync(x => x.StateId == id);

            return State;
        }

        [HttpPost()]
        public async Task<State> CreateState([FromBody] State state)
        {
            databaseContext.States.Add(state);
            await databaseContext.SaveChangesAsync();

            return state;
        }

        [HttpPut("{id}")]
        public async Task<State?> UpdateState([FromRoute] int id, [FromBody] State state)
        {

            var stateRecord = await databaseContext.States.FirstOrDefaultAsync(x => x.StateId == id);

            if (stateRecord == null)
            {
                return null;
            }

            stateRecord.StateCode = state.StateCode;
            stateRecord.StateName = state.StateName;

            await databaseContext.SaveChangesAsync();

            return stateRecord;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteState([FromRoute] int id)
        {
            var stateRecord = await databaseContext.States.FirstOrDefaultAsync(x => x.StateId == id);

            if (stateRecord == null)
            {
                return false;
            }

            databaseContext.States.Remove(stateRecord);

            await databaseContext.SaveChangesAsync();

            return true;

        }
    }
}


