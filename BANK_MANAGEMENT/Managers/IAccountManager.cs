using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;

namespace BANK_MANAGEMENT.Repo
{
    public interface IAccountManager
    {
        Task<(int, string)> AddAccount(AccountPostDto accountPostDto);
        Task<(int, string)> AddAccountOnly(AccountDto accountDto);
        Task<(int, string)> DeactiveAccount(decimal accountNumber);
        Task<(int, string)> DeleteAccount(decimal accountnumber);
        Task<(int, string, Account)> GetAccoount(decimal accountNumber);
    }
}