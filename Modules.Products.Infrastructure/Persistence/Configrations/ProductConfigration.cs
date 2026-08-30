  using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Products.Domain;
using System;

namespace Modules.Products.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Cədvəlin adı
        builder.ToTable("Products");

        // Əsas xüsusiyyətlər (Properties)
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Price).IsRequired();

        // ProductDescription ilə One-to-One (Birə-Bir) əlaqəsi
        builder.HasOne(p => p.ProductDescription)
               .WithOne(pd => pd.Product)
               .HasForeignKey<ProductDescription>(pd => pd.Id);

        // Qlobal Query Filter (Silinmişləri gətirmə)
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasMany(p => p.ProductPriceHistories)
            .WithOne(ph => ph.Product)
            .HasForeignKey(ph => ph.Id);    
        // Data Seed (Başlanğıc məlumatları)
        var seedDate = new DateTimeOffset(2026, 8, 17, 0, 0, 0, TimeSpan.Zero);

        builder.HasData(
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
}