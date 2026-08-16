using Common.Entites;
using Microsoft.EntityFrameworkCore;
using Modules.Categories.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Categories.Infrastructure.Persistence;

public class CategoriesDbContext : DbContext
{
    public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Categories");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
        var seedDate = new DateTimeOffset(2026, 8, 17, 0, 0, 0, TimeSpan.Zero);

        // Kateqoriyalar sənin dediyin kimi 2-dən başlayır
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 2, Name = "Elektronika", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 3, Name = "Geyim", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 4, Name = "Ev və Mebel", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 5, Name = "İdman və Əyləncə", CreatedAt = seedDate, IsDeleted = false }
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