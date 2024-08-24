using Microsoft.EntityFrameworkCore;
using PaymentGatewayService.Data.Configuration;
using PaymentGatewayService.Models.Entities;

namespace PaymentGatewayService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Payment>? Payments { get; set; }
        public DbSet<Setting>? Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new SettingConfiguration());
            modelBuilder.ApplyConfiguration(new SeedData());
        }
    }
}