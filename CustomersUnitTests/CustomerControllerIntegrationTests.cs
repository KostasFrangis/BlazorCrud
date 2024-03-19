using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Server.Services;
using System.Net;
using Pocos;

namespace CustomersUnitTests
{
    public class ApiIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly TokenService _tokenService;

        public ApiIntegrationTests()
        {
            // Initialize HttpClient with the base URL of your API
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5445/")
            };
        }

        public void Dispose()
        {
            // Clean up HttpClient
            _client.Dispose();
        }

        [Fact]
        public async Task GetRequest_ReturnsSuccessStatusCode()
        {
            // Arrange
            var requestUri = "api/customer?pageNumber=1&pageSize=10"; // Specify the endpoint you want to test
            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostRequest_ReturnsUnauthorizedStatusCode()
        {
            // Arrange
            var requestUri = "api/customer"; // Specify the endpoint you want to test
            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyName = "Company 11",
                Address = "Ethnikis Amynis 26",
                City = "Thiva",
                ContactName = "Maria",
                Country = "Greece",
                Phone = "2410729456",
                PostalCode = "41222",
                Region = "Sterea Ellada"
            };
            
            var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(customer); // Sample JSON request body

            // Convert request body to StringContent
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUri, content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PutRequest_ReturnsUnauthorizedStatusCode()
        {
            // Arrange
            var requestUri = "api/customer/"; // Specify the endpoint you want to test
            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyName = "Company 11",
                Address = "Ethnikis Amynis 26",
                City = "Thiva",
                ContactName = "Maria",
                Country = "Greece",
                Phone = "2410729456",
                PostalCode = "41222",
                Region = "Sterea Ellada"
            };

            var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(customer); // Sample JSON request body

            // Convert request body to StringContent
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(requestUri + customer.Id, content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task DeleteRequest_ReturnsUnauthorizedStatusCode()
        {
            // Arrange
            var requestUri = "api/customer/"+ Guid.NewGuid(); // Specify the endpoint you want to test

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}
