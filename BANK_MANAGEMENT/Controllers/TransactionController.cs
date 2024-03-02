using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BANK_MANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionManager _transactionManager;


        public TransactionController(ITransactionManager transactionRepo)
        {
            _transactionManager = transactionRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTransaction()
        {
            var result = await _transactionManager.GetAllTransactions();
            return Ok(result);
            ;
        }

        [HttpGet("{accountNumber:decimal}")]

        public async Task<IActionResult> GetAllTransaciton([FromRoute] decimal accountNumber)
        {
            var result = await _transactionManager.GetAllTransacitonsByAccount(accountNumber);
            return Ok(result);
        }

        [HttpPost("Deposite")]

        public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
        {
            var result = await _transactionManager.AddTransaction(transactionDTO);
            if (result.Item1 == 0) return BadRequest(result.Item2);
            return Ok(result.Item2);
        }

        [HttpPost("Transfer")]

        public async Task<IActionResult> TransferMoney([FromBody] TransactionDTO transactionDTO)
        {
            var result = await _transactionManager.TransferMoney(transactionDTO);
            if (result.Item1 == 0) return BadRequest(result.Item2);
            return Ok(result.Item2);
        }

        [HttpGet("transactionId/{transactionId:decimal}")]
        public async Task<IActionResult> GetTransaciton([FromRoute] decimal transactionId)
        {
            var result = await _transactionManager.GetTransactionById(transactionId);
            if (result.Item1 == 0) return NotFound(result.Item3);
            return Ok(result.Item2);
        }



    }
}
