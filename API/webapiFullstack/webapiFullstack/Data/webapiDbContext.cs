using Microsoft.EntityFrameworkCore;
using webapiFullstack.Models;

namespace webapiFullstack.Data
{
    public class webapiDbContext : DbContext
    {
        public webapiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
