using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Helpers
{
    public class TestDbcontext : DbContext
    {
        public TestDbcontext(DbContextOptions<TestDbcontext>options): base(options) 
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

    }
}
