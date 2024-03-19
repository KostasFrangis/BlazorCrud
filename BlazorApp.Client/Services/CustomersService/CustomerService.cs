using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Pocos;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using IdentityModel.Client;

namespace BlazorApp.Client.Services.CustomersService
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public CustomerService(HttpClient http, NavigationManager navigationManager, IConfiguration configuration, ITokenService tokenService)
        {
            _http = http;
            _navigationManager = navigationManager;
            Customers = new List<CustomerDTO>();
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public List<CustomerDTO> Customers { get; set; }
        
        public List<CustomerDTO> PagedCustomers { get; set; }
        private string _accessToken;
        public int TotalCustomers { get; set; }

        private async Task<string> GetAccessToken()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                var tokenResponse = await _tokenService.GetToken("CustomersAPI.read");
                _accessToken = tokenResponse.AccessToken;
            }
            return _accessToken;
        }

        public async Task GetPagedCustomers(int pageNumber, int pageSize)
        {
            try
            {
                string accessToken = await GetAccessToken();
                _http.SetBearerToken(accessToken);
                string url = _configuration["apiUrl"] + "/api/customer?pageNumber=" + pageNumber + "&pageSize=" + pageSize;
                var result =
                    await _http.GetFromJsonAsync<List<CustomerDTO>>(url);

                if (result != null)
                {
                    PagedCustomers = result;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task CreateCustomer(CustomerDTO customer)
        {
            try
            {
                string accessToken = await GetAccessToken();
                _http.SetBearerToken(accessToken);
                var result = await _http.PostAsJsonAsync(_configuration["apiUrl"] + "/api/customer", customer);
                await SetCustomers(result);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        private async Task SetCustomers(HttpResponseMessage result)
        {
            string accessToken = await GetAccessToken();
            _http.SetBearerToken(accessToken);
            var response = await result.Content.ReadFromJsonAsync<List<CustomerDTO>>();
            Customers = response;
            _navigationManager.NavigateTo("customers");
        }

        public async Task DeleteCustomer(Guid id)
        {
            string accessToken = await GetAccessToken();
            _http.SetBearerToken(accessToken);
            var result = await _http.DeleteAsync(_configuration["apiUrl"] + "/api/customer/" + id);
            await SetCustomers(result);
        }

        public async Task<CustomerDTO> GetSingleCustomer(Guid id)
        {
            string accessToken = await GetAccessToken();
            _http.SetBearerToken(accessToken);
            var result = await _http.GetFromJsonAsync<CustomerDTO>(_configuration["apiUrl"] + "/api/customer/" + id);
            if (result != null)
                return result;
            throw new Exception("Customer not found!");
        }

        public async Task GetCustomerCount()
        {
            try
            {
                string accessToken = await GetAccessToken();
                _http.SetBearerToken(accessToken);
                var result = await _http.GetFromJsonAsync<int>(_configuration["apiUrl"] + "/api/customer/customerCount");
                if (result != null)
                    TotalCustomers = result;
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateCustomer(CustomerDTO customer)
        {
            try
            {
                string accessToken = await GetAccessToken();
                _http.SetBearerToken(accessToken);
                var result = await _http.PutAsJsonAsync(_configuration["apiUrl"] + "/api/customer/{customer.Id}", customer);
                await SetCustomers(result);
            }
            catch (Exception ex)
            {

            }
        }

    }

}
