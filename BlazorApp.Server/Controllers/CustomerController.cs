using BlazorApp.Server.Repos.DBContext;
using BlazorApp.Server.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Pocos;
using System.Data;


namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly EntityDbContext.DataContext _context;
        private readonly CustomerService _service;
        private readonly string _connectionString;

        public CustomerController(EntityDbContext.DataContext context, CustomerService service)
        {
            _context = context;
            _service = service;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _connectionString = configuration.GetConnectionString("ConnString");

        }
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            //get customers
            var query = _context.Customers.ToList();
            //do the actual paging
            int skipCount = (pageNumber - 1) * pageSize;
            var pagedCustomers = query.Skip(skipCount).Take(pageSize).ToList();
            var res = pagedCustomers.Select(customer => _service.CopyDto(customer)).ToList();
            return Ok(res);
        }

        [HttpGet("customerCount")]
        public async Task<ActionResult<int>> CustomerCount()
        {
            //Use Dapper instead of EF
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Customers";
                int count = await dbConnection.ExecuteScalarAsync<int>(query);
                return Ok(count);
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound("Customer  not found.");
            CustomerDTO cus = _service.CopyDto(customer);
            return Ok(cus);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            List<CustomerDTO> res = new List<CustomerDTO>();
            foreach (var cus in _context.Customers)
                res.Add(_service.CopyDto(cus));
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer customer)
        {
            var dbCustomer = await _context.Customers.FindAsync(customer.Id);
            if (dbCustomer == null)
                return BadRequest("Customer not found.");

            dbCustomer.Address = customer.Address;
            dbCustomer.City = customer.City;
            dbCustomer.CompanyName = customer.CompanyName;
            dbCustomer.ContactName = customer.ContactName;
            dbCustomer.Country = customer.Country;
            dbCustomer.Phone = customer.Phone;
            dbCustomer.PostalCode = customer.PostalCode;
            dbCustomer.Region = customer.Region;

            await _context.SaveChangesAsync();
            List<CustomerDTO> res = new List<CustomerDTO>();
            foreach (var cus in _context.Customers)
                res.Add(_service.CopyDto(cus));
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> Delete(Guid id)
        {
            var dbCustomer = await _context.Customers.FindAsync(id);
            if (dbCustomer == null)
                return BadRequest("Customer not found.");

            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();
            List<CustomerDTO> res = new List<CustomerDTO>();
            foreach (var cus in _context.Customers)
                res.Add(_service.CopyDto(cus));
            return Ok(res);
        }
    }
}