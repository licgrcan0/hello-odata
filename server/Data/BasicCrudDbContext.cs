using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class BasicCrudDbContext : DbContext
{
    public BasicCrudDbContext(DbContextOptions<BasicCrudDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
}