using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;

namespace BANK_MANAGEMENT.Repo
{
    public interface ITransactionManager
    {
        Task<(int, string)> AddTransaction(TransactionDTO transactionDTO);
        Task<List<Transaction>> GetAllTransacitonsByAccount(decimal accountNumber);
        Task<List<Transaction>> GetAllTransactions();
        Task<(int, Transaction, string)> GetTransactionById(decimal transactionId);
        Task<(int, string)> TransferMoney(TransactionDTO transactionDTO);
    }
}