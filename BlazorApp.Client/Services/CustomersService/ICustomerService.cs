
using Pocos;

namespace BlazorApp.Client.Services.CustomersService
{
    public interface ICustomerService
    {
        List<CustomerDTO> PagedCustomers { get; set; }
        int TotalCustomers { get; set; }
        Task GetPagedCustomers(int pageNumber, int pageSize);
        Task GetCustomerCount();
        Task<CustomerDTO> GetSingleCustomer(Guid id);
        Task CreateCustomer(CustomerDTO customer);
        Task UpdateCustomer(CustomerDTO customer);
        Task DeleteCustomer(Guid id);
    }
}
