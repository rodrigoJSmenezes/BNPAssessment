using BNPTest.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace BNPTest.Infrastructure
{
    public class ApiContext : DbContext
    {
        public DbSet<Security> Securities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SecuritiesDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
