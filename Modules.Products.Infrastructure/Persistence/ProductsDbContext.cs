using Common.Entites;
using Microsoft.EntityFrameworkCore;
using Modules.Products.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Products.Infrastructure.Persistence;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDescription> ProductDescriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Products");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Price).IsRequired();
        });

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductDescription)
            .WithOne(pd => pd.Product)
            .HasForeignKey<ProductDescription>(pd => pd.Id);

        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<ProductDescription>().HasQueryFilter(x => !x.IsDeleted);

        var seedDate = new DateTimeOffset(2026, 8, 17, 0, 0, 0, TimeSpan.Zero);

        modelBuilder.Entity<Product>().HasData(
           // CategoryId = 2 (Elektronika)
           new Product { Id = 101, Name = "Noutbuk Asus ROG", Price = 2499.99, CategoryId = 2, CreatedAt = seedDate, IsDeleted = false },
           new Product { Id = 102, Name = "Apple iPhone 15 Pro", Price = 2799.00, CategoryId = 2, CreatedAt = seedDate, IsDeleted = false },
           new Product { Id = 103, Name = "Simsiz Qulaqlıq AirPods", Price = 450.00, CategoryId = 2, CreatedAt = seedDate, IsDeleted = false },

           // CategoryId = 3 (Geyim)
           new Product { Id = 104, Name = "Kişi Qış Gödəkcəsi", Price = 120.50, CategoryId = 3, CreatedAt = seedDate, IsDeleted = false },
           new Product { Id = 105, Name = "Qadın Donu", Price = 85.00, CategoryId = 3, CreatedAt = seedDate, IsDeleted = false },

           // CategoryId = 4 (Ev və Mebel)
           new Product { Id = 106, Name = "Ortopedik Matras", Price = 300.00, CategoryId = 4, CreatedAt = seedDate, IsDeleted = false },
           new Product { Id = 107, Name = "İş Masası", Price = 150.00, CategoryId = 4, CreatedAt = seedDate, IsDeleted = false },

           // CategoryId = 5 (İdman və Əyləncə)
           new Product { Id = 108, Name = "Qaçış Trenajoru", Price = 800.00, CategoryId = 5, CreatedAt = seedDate, IsDeleted = false },
           new Product { Id = 109, Name = "Futbol Topu (Nike)", Price = 65.00, CategoryId = 5, CreatedAt = seedDate, IsDeleted = false }
       );
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