using Microsoft.EntityFrameworkCore;
using Modules.Baskets.Domain;
using Modules.Baskets.Infrastructure.Persistence.Configurations;
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BasketConfiguration).Assembly);
     
    }
}