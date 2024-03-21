using Employee_Domain.Entity;
using System;
using System.Data.Entity;

namespace Employee_Infrastructure.Contex
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    
    }
}
