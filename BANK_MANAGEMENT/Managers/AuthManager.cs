using BANK_MANAGEMENT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BANK_MANAGEMENT.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly BankContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthManager(BankContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<(int, string)> SignUp(UserModel user)
        {
            if (user.Password.Length < 6) return (0, "Enter minimun 6 digit password");
           
            _dbContext.User.Add(user);
            
            try
            {
                await _dbContext.SaveChangesAsync();
                return (1, "Register Successfully");
            }
            catch (DbUpdateException ex)
            {
                return (2, "User is Already Exist");
            }

            catch (Exception ex)
            {
                return (2, ex.GetType().ToString());
            }
        }

        public async Task<(int, string, UserModel)> Login(UserModel userModel)
        {
            try
            {
                var user = await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == userModel.UserName && u.Password == userModel.Password);
                if (user != null) return (1, "Login", user);
                return (0, "User Not Found", null);

            }
            catch (Exception ex)
            {
                return (2, ex.Message, null);

            }
        }

        public string GenerateToken(UserModel user)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credencial = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Password),
                new Claim(ClaimTypes.Role,user.Role == 1 ? "Admin":"User")
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
               expires: DateTime.Now.AddMinutes(-4),
               signingCredentials: credencial

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
