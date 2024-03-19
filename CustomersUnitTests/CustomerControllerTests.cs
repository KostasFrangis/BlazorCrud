using BlazorApp.Server.Controllers;
using BlazorApp.Server.Repos.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pocos;
using BlazorApp.Server.Services;
using FluentAssertions;

namespace CustomersUnitTests
{
    public class CustomerControllerTests
    {
        private CustomerController _customerController;
        private EntityDbContext.DataContext _mockDataContext;
        private readonly DbContextOptions<EntityDbContext.DataContext> _options;

        public CustomerControllerTests()
        {
            _options = new DbContextOptionsBuilder<EntityDbContext.DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new EntityDbContext.DataContext(_options))
            {
                if (!context.Customers.Any())
                {
                    var customers = new List<Customer>
                    {
                        new Customer
                        {
                            Id = new Guid("078236bc-2c86-47a2-84ff-eee837785400"), CompanyName = "Company 1",
                            Address = "Makri 15", City = "Thessaloniki", ContactName = "Kostas", Country = "Greece",
                            Phone = "2310621456", PostalCode = "56625", Region = "Sykies"
                        },
                        new Customer
                        {
                            Id = new Guid("078236bc-2c86-47a2-84ff-eee837785401"), CompanyName = "Company 2", Address = "Aristotelous 22",
                            City = "Athens", ContactName = "Maria", Country = "Greece", Phone = "2109876543",
                            PostalCode = "11528", Region = "Attica"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 3", Address = "Papadiamanti 7",
                            City = "Heraklion", ContactName = "Nikos", Country = "Greece", Phone = "2810234567",
                            PostalCode = "71304", Region = "Crete"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 4", Address = "Agiou Dimitriou 10",
                            City = "Patras", ContactName = "Eleni", Country = "Greece", Phone = "2610789456",
                            PostalCode = "26441", Region = "Western Greece"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 5", Address = "Agiou Konstantinou 5",
                            City = "Volos", ContactName = "Giannis", Country = "Greece", Phone = "2420567890",
                            PostalCode = "38221", Region = "Thessaly"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 6", Address = "Agiou Spyridonos 18",
                            City = "Ioannina", ContactName = "Sophia", Country = "Greece", Phone = "2651045678",
                            PostalCode = "45221", Region = "Epirus"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 7", Address = "Pavlou Mela 3",
                            City = "Kalamata", ContactName = "Dimitris", Country = "Greece", Phone = "2721034567",
                            PostalCode = "24100", Region = "Peloponnese"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 8", Address = "Vasileos Georgiou 12",
                            City = "Chania", ContactName = "Anna", Country = "Greece", Phone = "2821045678",
                            PostalCode = "73100", Region = "Crete"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 9", Address = "Tsimiski 25",
                            City = "Thessaloniki", ContactName = "Panagiotis", Country = "Greece", Phone = "2310543210",
                            PostalCode = "54623", Region = "Sykies"
                        },
                        new Customer
                        {
                            Id = Guid.NewGuid(), CompanyName = "Company 10", Address = "Ethnikis Antistaseos 6",
                            City = "Larissa", ContactName = "Eva", Country = "Greece", Phone = "2410789456",
                            PostalCode = "41222", Region = "Thessaly"
                        }
                    };
                    foreach (var customer in customers)
                        context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }

        }

        [Fact]
        public async Task GetSpecificCustomer_ReturnsNotFound()
        {

            using (var context = new EntityDbContext.DataContext(_options))
            {
                var controller = new CustomerController(context, new CustomerService());

                // Act
                var result = await controller.Get(new Guid());

                // Assert
                Assert.IsType<NotFoundObjectResult>(result.Result);
            }
        }


        [Fact]
        public async Task GetSpecificCustomer_ReturnsCustomer()
        {

            using (var context = new EntityDbContext.DataContext(_options))
            {
                var controller = new CustomerController(context, new CustomerService());

                // Act
                var result = await controller.Get(new Guid("078236bc-2c86-47a2-84ff-eee837785400"));

                // Assert
                Assert.NotNull(result);
                Assert.Equal(new Guid("078236bc-2c86-47a2-84ff-eee837785400"), ((CustomerDTO)((ObjectResult)result.Result).Value).Id);
            }
        }
        [Fact]
        public async Task GetAllCustomers_WithExistingCustomers_ReturnsCustomers()
        {
            using (var context = new EntityDbContext.DataContext(_options))
            {
                var controller = new CustomerController(context, new CustomerService());

                // Act
                var result = await controller.Get(1, 10);

                // Assert

                var list = (List<CustomerDTO>)((ObjectResult)result.Result).Value;

                list.Should().HaveCount(context.Customers.Count());
                list.Select(c => c.Id).Should().BeEquivalentTo(context.Customers.Select(c => c.Id));
                list.Should().BeEquivalentTo(context.Customers);

            }
        }

        [Fact]
        public async Task CreateCustomer_ReturnCustomers()
        {
            using (var context = new EntityDbContext.DataContext(_options))
            {
                var controller = new CustomerController(context, new CustomerService());
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
                // Act
                var result = await controller.AddCustomer(customer);

                // Assert
                var list = (List<CustomerDTO>)((ObjectResult)result.Result).Value;
                list.Should().BeEquivalentTo(context.Customers);

            }
        }

        [Fact]
        public async Task UpdateCustomer_ReturnCustomers()
        {
            using (var context = new EntityDbContext.DataContext(_options))
            {
                var controller = new CustomerController(context, new CustomerService());
                Customer customer = new Customer
                {
                    Id = new Guid("078236bc-2c86-47a2-84ff-eee837785400"),
                    CompanyName = "Company 1",
                    Address = "Makri 25",
                    City = "Thessaloniki",
                    ContactName = "Kostas",
                    Country = "Greece",
                    Phone = "2310621456",
                    PostalCode = "56625",
                    Region = "Sykies"
                };
                // Act
                var result = await controller.UpdateCustomer(customer);

                // Assert
                var list = (List<CustomerDTO>)((ObjectResult)result.Result).Value;
                list.Should().BeEquivalentTo(context.Customers);
            }
        }

        [Fact]
        public async Task DeleteCustomer_ReturnCustomers()
        {
            using (var context = new EntityDbContext.DataContext(_options))
            {
                var controller = new CustomerController(context, new CustomerService());
                // Act
                var result = await controller.Delete(new Guid("078236bc-2c86-47a2-84ff-eee837785401"));

                // Assert
                var list = (List<CustomerDTO>)((ObjectResult)result.Result).Value;
                list.Should().BeEquivalentTo(context.Customers);
                list.Should().HaveCount(9);
            }
        }

    }

}

