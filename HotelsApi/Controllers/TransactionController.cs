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
    [Route("Transaction")]

    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllTransaction()
        {
            var transactions = await transactionService.GetAllTransaction();

            return Ok(transactions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
        {
            var transaction = await transactionService.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransaction transaction)
        {
            var createdCard = await transactionService.CreateTransaction(transaction);

            if (createdCard == null)
                return BadRequest();

            return Ok(createdCard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction([FromRoute] int id, [FromBody] UpdateTransaction transaction)
        {
            transaction.TransactionId = id;
            var updateTransactionResult = await transactionService.UpdateTransaction(id, transaction);

            if (updateTransactionResult == null)
                return BadRequest();

            return Ok(updateTransactionResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            var deleteResult = await transactionService.DeleteTransaction(id);
            if (deleteResult == false)
                return BadRequest();

            return Ok(deleteResult);
        }
    }
}


