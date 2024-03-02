using BANK_MANAGEMENT.Models;

namespace BANK_MANAGEMENT.Managers
{
    public interface IAuthManager
    {
        Task<(int, string)> SignUp(UserModel user);
        Task<(int, string, UserModel)> Login(UserModel userModel);

        string GenerateToken(UserModel user);
    }
}