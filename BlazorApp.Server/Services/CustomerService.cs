using Pocos;

namespace BlazorApp.Server.Services
{
    public class CustomerService
    {
        public CustomerService()
        {
            
        }

        public CustomerDTO CopyDto(Customer dao)
        {
            return new CustomerDTO
            {
                Id = dao.Id,
                CompanyName = dao.CompanyName,
                ContactName = dao.ContactName,
                Address = dao.Address,
                Region = dao.Region,
                City = dao.City,
                PostalCode = dao.PostalCode,
                Country = dao.Country,
                Phone = dao.Phone,
            };
        }
    }
}
