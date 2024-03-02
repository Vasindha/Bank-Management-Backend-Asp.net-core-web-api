using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Managers;
using BANK_MANAGEMENT.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace BANK_MANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        
        private readonly IAuthManager _authManger;
        public AuthController( IAuthManager authManager)
        {
            
            
            _authManger = authManager;
        }


        [HttpPost("Login")]
       public async Task<IActionResult> Login(UserModel user)
        {
            IActionResult response = Unauthorized();
            var result = await _authManger.Login(user);
            if (result.Item1 == 1)
            {
                var token = _authManger.GenerateToken(result.Item3);
                return Ok(token);
            }
            if(result.Item1 == 0) return NotFound(result.Item2);
            return BadRequest(response);
        }


        [HttpPost ("SignUp")]
       public async Task<IActionResult> SignUp(UserModel user)
        {
           var result = await _authManger.SignUp(user);
            if( result.Item1 == 0) { return BadRequest(result.Item2); }
            if(result.Item1 == 1) { return Ok(result.Item2); }
            return BadRequest(result.Item2);

        }

       

    }
}
