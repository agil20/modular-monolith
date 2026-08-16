using Microsoft.EntityFrameworkCore;
using Modules.Baskets.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Baskets.Infrastructure.Persistence;

public class BasketDbContext : DbContext
{
    public BasketDbContext(DbContextOptions<BasketDbContext> options)
        : base(options)
    {
    }

    public DbSet<Modules.Baskets.Domain.Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Baskets");

        modelBuilder.Entity<Modules.Baskets.Domain.Basket>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).ValueGeneratedOnAdd();
            entity.HasMany(b => b.Items)
                  .WithOne(i => i.Basket)
                  .HasForeignKey(i => i.BasketId);
        });
    }
}