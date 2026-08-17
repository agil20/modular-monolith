using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Categories.Domain;
using System;

namespace Modules.Categories.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
    
        builder.ToTable("Categories");

     
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.HasQueryFilter(x => !x.IsDeleted);

        var seedDate = new DateTimeOffset(2026, 8, 17, 0, 0, 0, TimeSpan.Zero);

        builder.HasData(
            new Category { Id = 2, Name = "Elektronika", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 3, Name = "Geyim", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 4, Name = "Ev və Mebel", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = 5, Name = "İdman və Əyləncə", CreatedAt = seedDate, IsDeleted = false }
        );
    }
}