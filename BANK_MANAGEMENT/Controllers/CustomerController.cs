using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BANK_MANAGEMENT.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerRepo)
        {
            _customerManager = customerRepo;
        }

        [HttpPost]

        public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            var result = await _customerManager.AddCustomer(customerDTO);
            if (result.Item1 == 0) return BadRequest(result.Item2);
            return Ok("Added Successfully");

        }

        //[HttpGet]

        //public async Task<IActionResult> GetAllCustomers()
        //{
        //    var customers = await _customerRepo.GetAllCustomers();
        //    if(customers != null) return Ok(customers);
        //    return BadRequest("Something went wrong");
        //}
        [HttpGet("{customerId:decimal}")]

        public async Task<IActionResult> GetAllAccounts([FromRoute] decimal customerId)
        {
            var result = await _customerManager.GetAccounts(customerId);
            if (result.Item1 == 0) return BadRequest(result.Item3);
            return Ok(result.Item2);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCustomerModel()
        {
            var result = await _customerManager.GetAllCustomers();
            if (result.Item1 == 1) return Ok(result.Item2);
            return BadRequest(result.Item3);
        }
        [HttpGet("aadhar/{aadharNumber:decimal}")]

        public async Task<IActionResult> GetCustomerByAadharNumber([FromRoute] decimal aadharNumber)
        {
            var result = await _customerManager.GetCustomerByAadharOrAccount(aadharNumber);
            if (result.Item1 == 0) return NotFound(result.Item3);
            if (result.Item1 == 1) return Ok(result.Item2);
            return BadRequest(result.Item3);
        }
        [HttpGet("{name}")]

        public async Task<IActionResult> GetCustomerByName([FromRoute] string name)
        {
            var result = await _customerManager.GetCustomerByName(name);

            return Ok(result);
        }

        [HttpGet("cust-accounts")]

        public async Task<IActionResult> GetAllCustomersAndAccount()
        {
            var customers = await _customerManager.GetCustomerAndAccount();
            if (customers != null) return Ok(customers);
            return BadRequest("Something went wrong");
        }


        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            var result = await _customerManager.UpdateCustomer(id, customerDTO);
            if (result.Item1 == 0)
                return NotFound(result.Item2);
            if (result.Item1 == 1) return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> DeletCustomer(int id)
        {
            var result = await _customerManager.DeleteCustomer(id);
            if (result.Item1 == 0) return NotFound(result.Item2);
            if (result.Item1 == 1) return Ok(result.Item2);
            return BadRequest(result.Item2);
        }
    }
}
