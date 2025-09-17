using EmployeeAPIWithJWT_123.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPIWithJWT_123.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
