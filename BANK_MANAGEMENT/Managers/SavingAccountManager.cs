using BANK_MANAGEMENT.Models;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Managers
{
    public class SavingAccount : IAccountType
    {
        private readonly BankContext _dbContext;
        public SavingAccount(BankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Id { get { return 0; } }

        public async Task<double> getInterestRate()
        {
            var rate = await _dbContext.InterestRates.FirstOrDefaultAsync(i => i.AccountType == 0);
            return rate.InterestRate;


        }
    }
}
