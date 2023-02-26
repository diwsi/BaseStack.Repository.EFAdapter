using Microsoft.EntityFrameworkCore;

namespace EFAdapter.Tests.Models
{
    internal class TestDBContext : DbContext
    { 
        public DbSet<TestEnity> TestEnities { get; set; }                      

        public TestDBContext(DbContextOptions<TestDBContext> options):base(options)
        { 

        }
 

    }
}
