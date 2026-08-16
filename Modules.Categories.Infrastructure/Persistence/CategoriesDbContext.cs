
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
                    // Səndə break sözü yuxarıda qalmışdı deyə xəta verəcəkdi, yeri düzəldildi
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    // Silinmə vaxtını da yenilənmə kimi qeyd etmək tövsiyə olunur
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}