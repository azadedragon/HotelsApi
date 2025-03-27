using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Controllers
{
    [ApiController]
    [Route("State")]

    public class StateController : ControllerBase
    {
        private readonly IStateService stateServices;

        public StateController(IStateService stateService)
        {
            this.stateServices = stateService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllState()
        {
            var states = await stateServices.GetAllStates();

            return Ok(states);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateById([FromRoute] int id)
        {
            var state = await stateServices.GetStateById(id);
            if (state == null)
                return NotFound();

            return Ok(state);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateState([FromBody] CreateState state)
        {
            var createdCard = await stateServices.CreateState(state);

            if (createdCard == null)
                return BadRequest();

            return Ok(createdCard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState([FromRoute] int id, [FromBody] UpdateState state)
        {
            state.StateId = id;
            var updateStateResult = await stateServices.UpdateState(id, state);

            if (updateStateResult == null)
                return BadRequest();

            return Ok(updateStateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState([FromRoute] int id)
        {
            var deleteResult = await stateServices.DeleteState(id);
            if (deleteResult == false)
                return BadRequest();

            return Ok(deleteResult);
        }
    }
}


