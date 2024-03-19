using Microsoft.EntityFrameworkCore;
using Pocos;

namespace BlazorApp.Server.Repos.DBContext
{
    public class EntityDbContext
    {
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Customer> Customers { get; set; }
        }
    }
}
