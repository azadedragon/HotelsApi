using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllStates();
        Task<State?> GetStateById(int id);
        Task<State> CreateState(State state);
        Task<State?> UpdateState(int id, State state);
        Task<bool> DeleteState(int id);
        Task<bool> StateCodeExists(string stateCode, CancellationToken cancellationToken = default);
        Task<bool> StateNameExists(string stateName, CancellationToken cancellationToken = default);
        Task<bool> StateExistsAsync(int stateId, CancellationToken cancellationToken = default);
    }
    public class StateRepository : IStateRepository
    {
        private readonly DatabaseContext databaseContext;
        public StateRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<State> CreateState(State state)
        {
            databaseContext.States.Add(state);
            await databaseContext.SaveChangesAsync();
            return state;
        }

        public async Task<bool> DeleteState(int id)
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

        public async Task<List<State>> GetAllStates()
        {
            var State = await databaseContext.States.ToListAsync();
            return State;
        }

        public async Task<State?> GetStateById(int id)
        {
            var State = await databaseContext.States.FirstOrDefaultAsync(x => x.StateId == id);
            return State;
        }

        public async Task<State?> UpdateState(int id, State state)
        {
            var stateRecord = await databaseContext.States.FirstOrDefaultAsync(x => x.StateId == id);
            if (stateRecord == null)
            {
                return null;
            }
            stateRecord.StateCode = state.StateCode;
            stateRecord.StateName = state.StateName;
            stateRecord.CountryId = state.CountryId;
            await databaseContext.SaveChangesAsync();
            return stateRecord;
        }
        public async Task<bool> StateCodeExists(string stateCode, CancellationToken cancellationToken = default)
        {
            return await databaseContext.States
                .AnyAsync(h => h.StateCode == stateCode, cancellationToken);
        }
        public async Task<bool> StateNameExists(string stateName, CancellationToken cancellationToken = default)
        {
            return await databaseContext.States
                .AnyAsync(h => h.StateName == stateName, cancellationToken);
        }
        public async Task<bool> StateExistsAsync(int stateId, CancellationToken cancellationToken = default)
        {
            return await databaseContext.States.AnyAsync(s => s.StateId == stateId, cancellationToken);
        }
    }
}
