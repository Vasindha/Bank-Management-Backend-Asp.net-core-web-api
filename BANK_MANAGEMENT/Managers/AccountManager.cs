using AutoMapper;
using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Repo
{
    public class AccountManager : IAccountManager
    {
        private readonly IMapper _mapper;
        private readonly BankContext _dbContext;
       
       
        public AccountManager(IMapper mapper, BankContext bankContext  )
        {
            _mapper = mapper;
            _dbContext = bankContext;
            
        }

        public async Task<(int, string)> AddAccountOnly(AccountDto accountDto)
        {
            try
            {
                if (accountDto.AccountType == 0)
                {
                    accountDto.InterestRate = 7;
                }
                else
                {
                    accountDto.InterestRate = 0;

                }
                var account = _mapper.Map<Account>(accountDto);
                _dbContext.Accounts.Add(account);
                await _dbContext.SaveChangesAsync();
                return (1, "AccountAdded Successfully");
            }
            catch (Exception ex)
            {
                return (0, ex.Message);
            }

        }
        public async Task<(int, string)> AddAccount(AccountPostDto accountPostDto)
        {

            var customer = accountPostDto;
           

            try
            {
                int change = await _dbContext.Database.ExecuteSqlRawAsync($"EXEC ADDACCOUNT  @CUST_FIRSTNAME, @CUST_MIDDLENAME,@CUST_LASTNAME,@CUST_ADDRESS,@DOB,@MOBILE,@EMAIL,@AADHAR,@ACCOOUNT_TYPE",
                      new SqlParameter("@CUST_FIRSTNAME", customer.CustomerFirstname),
                      new SqlParameter("@CUST_MIDDLENAME", customer.CustomerMiddletname),
                       new SqlParameter("@CUST_LASTNAME", customer.CustomerLastname),
                      new SqlParameter("@CUST_ADDRESS", customer.CustomerAddress),
                       new SqlParameter("@DOB", customer.CustomerDob),
                      new SqlParameter("@MOBILE", customer.CustomerMobileno),
                      new SqlParameter("@EMAIL", customer.CustomerEmail),
                      new SqlParameter("@AADHAR", customer.CustomerAadharno),
                      new SqlParameter("@ACCOOUNT_TYPE", accountPostDto.AccountType)


                      );


                await _dbContext.SaveChangesAsync();

                return (1, "success");

            }
            catch (Exception ex)
            {

                return (0, ex.Message);
            }
        }

        public async Task<(int, string, Account)> GetAccoount(decimal accountNumber)
        {




            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account == null) return (0, "AccountNotFound", null);
            return (1, "Succes", account);


        }
        public async Task<(int, string)> DeactiveAccount(decimal accountNumber)
        {
            try
            {
                var account = await _dbContext.Accounts.FindAsync(accountNumber);
                if (account == null)
                {
                    return (0, "Account not found");
                }
                if (account.AccountStatus == 1)
                {
                    account.AccountStatus = 0;
                    await _dbContext.SaveChangesAsync();
                    return (1, "Activated!");
                }
                else
                {
                    account.AccountStatus = 1;
                    await _dbContext.SaveChangesAsync();
                    return (1, "deactivated");
                }


            }
            catch (Exception ex)
            {

                return (2, ex.Message);
            }

        }


        public async Task<(int, string)> DeleteAccount(decimal accountnumber)
        {
            try
            {
                var account = await _dbContext.Accounts.FindAsync(accountnumber);
                if (account == null) return (0, "Account Not Found");
                _dbContext.Remove(account);
                await _dbContext.SaveChangesAsync();
                return (1, "Account Deleted");

            }
            catch (Exception ex)
            {
                return (2, ex.Message);
            }
        }
        //public async Task<int> AddAccount(AccountPostDto accountPostDto)


    }
}
