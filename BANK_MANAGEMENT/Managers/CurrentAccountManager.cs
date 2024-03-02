using BANK_MANAGEMENT.Models;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Managers
{
    public class CurrentAccount : IAccountType
    {
        private readonly BankContext _dbContext;
        public CurrentAccount(BankContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public int Id { get { return 1; } }

        public async Task<double> getInterestRate()
        {
            var rate = await _dbContext.InterestRates.FirstOrDefaultAsync(i => i.AccountType == 1);
            return rate.InterestRate;


        }
    }
}
