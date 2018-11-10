using CompanyManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.DAL
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(c => c.CompanyId);
            
            base.OnModelCreating(builder);
        }
    }
}