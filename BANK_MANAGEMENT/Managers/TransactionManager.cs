using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Repo
{
    public class TransactionManager : ITransactionManager
    {
        private readonly BankContext _dbContext;
        public TransactionManager(BankContext context)
        {
            this._dbContext = context;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<(int, Transaction, string)> GetTransactionById(decimal transactionId)
        {
            try
            {
                var transaction = await _dbContext.Transactions.FindAsync(transactionId);
                if (transaction == null) { return (0, null, "Transaction Not Found"); }
                return (1, transaction, "success");

            }
            catch (Exception ex)
            {
                return (0, null, ex.Message);
            }
        }
        public async Task<List<Transaction>> GetAllTransacitonsByAccount(decimal accountNumber)
        {
            var transactions = await _dbContext.Transactions.Where(t => t.AccountNumber == accountNumber).ToListAsync();
            return transactions;
        }


        public async Task<(int, string)> AddTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync($"EXEC TRANSACTIONTOACCOUNT @AMOUNT,@ACCOUNT_NUMBER,@DESC,@TRANSACTION_TYPE",
                    new SqlParameter("@AMOUNT", transactionDTO.TransactionAmount),
                     new SqlParameter("@ACCOUNT_NUMBER", transactionDTO.AccountNumber),
                      new SqlParameter("@DESC", transactionDTO.TransactionDescription),

                       new SqlParameter("@TRANSACTION_TYPE", transactionDTO.TansactionType));

                return (1, transactionDTO.TansactionType == 0 ? "Deposite" : "Withdraw" + " Successfully");
            }
            catch (Exception ex)
            {

                return (0, ex.Message);
            }
        }

        public async Task<(int, string)> TransferMoney(TransactionDTO transactionDTO)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync($"EXEC TRANSFERAMOUNT @AMOUNT,@ACCOUNT_NUMBER,@RECEIVER_ACCOUNT_NUMBER,@DESC",
                    new SqlParameter("@AMOUNT", transactionDTO.TransactionAmount),
                     new SqlParameter("@ACCOUNT_NUMBER", transactionDTO.AccountNumber),
                      new SqlParameter("@DESC", transactionDTO.TransactionDescription),

                       new SqlParameter("@RECEIVER_ACCOUNT_NUMBER", transactionDTO.ReceiverAccountNumber));

                return (1, "Transfer Successfully");
            }
            catch (Exception ex)
            {

                return (0, ex.Message);
            }
        }


    }
}
