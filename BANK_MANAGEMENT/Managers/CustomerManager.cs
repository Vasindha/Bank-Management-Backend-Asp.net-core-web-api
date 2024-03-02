using AutoMapper;
using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace BANK_MANAGEMENT.Repo
{
    public class CustomerManager : ICustomerManager
    {
        private readonly BankContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerManager(BankContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<(int, string)> AddCustomer(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            await _dbContext.Customers.AddAsync(customer);
            try
            {

                await _dbContext.SaveChangesAsync();

                return (1, "Added Successfully");

            }
            catch (Exception ex)
            {

                return (0, ex.Message);
            }

        }

        public async Task<List<CustomerAccount>> GetCustomerAndAccount()
        {

            var customers = await _dbContext.CustomerAccounts.FromSqlInterpolated($"EXEC GETCUSTOMERS").ToListAsync();


            return customers;

        }

        public async Task<List<Customer>> GetCustomerByName(string name)
        {

            var c = await _dbContext.Customers.Where(c => EF.Functions.Like(c.CustomerFirstname, "%" + name + "%") || EF.Functions.Like(c.CustomerMiddletname, "%" + name + "%") || EF.Functions.Like(c.CustomerLastname, "%" + name + "%")).ToListAsync();
            return c;
        }

        public async Task<(int, Customer, string)> GetCustomerByAadharOrAccount(decimal aadharNumber)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerAadharno == aadharNumber);
                if (customer == null)
                {
                    var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == aadharNumber);

                    if (account != null)
                    {
                        Console.WriteLine("Account Is", account);
                        customer = await _dbContext.Customers.FirstAsync(c => c.CustomerId == account.CustomerId);
                        return (1, customer, "Success");
                    }
                    else
                    {
                        return (0, null, "Customer Not Found");
                    }


                }
                return (1, customer, "Success");


            }
            catch (Exception ex)
            {
                return (2, null, ex.ToString());
            }
        }

        public async Task<(int, List<Customer>, string)> GetAllCustomers()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                return (1, customers, "success");
            }
            catch (Exception ex)
            {
                return (0, null, ex.Message);
            }

        }


        public async Task<(int, string)> UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

                if (customer == null) { return (0, "Customer Not Found"); }

                _mapper.Map<CustomerDTO, Customer>(customerDTO, customer);


                await _dbContext.SaveChangesAsync();
                return (1, "Customer Updated");

            }
            catch (Exception ex)
            {
                return (2, ex.Message);
            }
        }
        public async Task<(int, string)> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

                if (customer != null)
                {
                    _dbContext.Customers.Remove(customer);
                    await _dbContext.SaveChangesAsync();
                    return (1, "Deleted Successfullly");
                }
                return (0, "Customer Not Found");

            }
            catch (Exception ex)
            {
                return (2, ex.Message);
            }
        }

        public async Task<(int, List<Account>, string)> GetAccounts(decimal customerId)
        {
            try
            {
                var accounts = await _dbContext.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
                return (1, accounts, "success");

            }
            catch (Exception ex)
            {
                return (0, null, ex.Message);
            }





        }
    }
}
