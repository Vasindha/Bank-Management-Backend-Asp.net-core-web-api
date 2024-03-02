using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Managers;
using BANK_MANAGEMENT.Models;
using BANK_MANAGEMENT.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace BANK_MANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [Authorize (Roles ="Admin")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
      private readonly IEnumerable<IAccountType> _accountTypes;
        public AccountController(IAccountManager accountRepo, IEnumerable<IAccountType> accountTypes)
        {
            _accountManager = accountRepo;
            _accountTypes = accountTypes;
        }

        [HttpGet("interest/{type}")]

        public async Task<IActionResult> GetInterest([FromRoute] int type)
        {
           foreach(var i in _accountTypes)
            {
                if(i.Id == type) return  Ok( await i.getInterestRate());

            }
            return NotFound("Account Type Not Found");
            
        }
        [HttpPost ("Account")]
        public async Task<IActionResult> AddAccountOnly([FromBody] AccountDto accountDto)
        {
            var result = await _accountManager.AddAccountOnly(accountDto);
            if (result.Item1 == 0) { return BadRequest(result.Item2); }

            return Ok(result.Item2);
        }

        [HttpPost]

        public async Task<IActionResult> AddAccount([FromBody] AccountPostDto accountPostDto)
        {
            var result = await _accountManager.AddAccount(accountPostDto);
            if(result.Item1 == 0) { return BadRequest(result.Item2); }

            return Ok("Added successfully");
        }

        [HttpGet ("{accountNumber:decimal}")]

        public async Task<IActionResult> GetAccountById( [FromRoute] decimal accountNumber)
        {
            var restul = await _accountManager.GetAccoount(accountNumber);
            if(restul.Item1 == 0) { return NotFound("Account Not Found"); }
            return Ok(restul);
        }

        [HttpDelete ("{accountNumber:decimal}")]

        public async Task<IActionResult> DeleteAccount([FromRoute] decimal accountNumber)
        {
            var result = await _accountManager.DeleteAccount(accountNumber);
            if(result.Item1 == 0) return NotFound(result.Item2);
            if (result.Item1 == 1) return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpPut]

        public async Task<IActionResult> DeactiveAccount([FromBody] decimal accountNumber)
        {
            var result = await _accountManager.DeactiveAccount(accountNumber);
            if (result.Item1 == 0) return NotFound(result.Item2);
            if (result.Item1 == 1) return Ok(result.Item2);
            return BadRequest(result.Item2);
        }


    }
}
