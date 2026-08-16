using Common.Entites;
using Microsoft.EntityFrameworkCore;
using Modules.Products.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Infrastructure.Persistence
{
    public class ProductsDbContext: DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Products");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Price).IsRequired();
            });

            modelBuilder.Entity<Product>().HasOne(p => p.ProductDescription)
                .WithOne(pd => pd.Product).HasForeignKey<ProductDescription>(pd => pd.Id);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<ProductDescription>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
