using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;

namespace BANK_MANAGEMENT.Repo
{
    public interface ICustomerManager
    {
        Task<(int, string)> AddCustomer(CustomerDTO customerDTO);
        Task<(int, string)> DeleteCustomer(int id);
        Task<(int, List<Account>, string)> GetAccounts(decimal customerId);
        Task<(int, List<Customer>, string)> GetAllCustomers();
        Task<List<CustomerAccount>> GetCustomerAndAccount();
        Task<(int, Customer, string)> GetCustomerByAadharOrAccount(decimal aadharNumber);
        Task<List<Customer>> GetCustomerByName(string name);
        Task<(int, string)> UpdateCustomer(int id, CustomerDTO customerDTO);
    }
}