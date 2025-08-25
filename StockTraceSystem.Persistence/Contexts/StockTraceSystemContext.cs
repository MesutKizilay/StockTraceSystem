using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace StockTraceSystem.Persistence.Contexts
{
    public class StockTraceSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }

        public StockTraceSystemContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}