using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.Entities;
using System.Reflection;

namespace ProductAPI.Data.Contexts
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(128);
                entity.Property(p => p.TypeId).IsRequired();
                entity.Property(p => p.Category).IsRequired();
            });
        }
    }
}