using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contex
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    
    }
}
