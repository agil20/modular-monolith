using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Baskets.Infrastructure.Persistence.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Modules.Baskets.Domain.Basket>
{
    public void Configure(EntityTypeBuilder<Modules.Baskets.Domain.Basket> builder)
    {
        // Cədvəl adını vermək həmişə yaxşı təcrübədir
        builder.ToTable("Baskets");

        // Primary Key (Əsas Açar)
        builder.HasKey(b => b.Id);

        // ID-nin avtomatik artması üçün (Onsuz da int üçün standartdır, amma yazmağın ziyanı yoxdur)
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        // Səbət (Basket) və Səbət Məhsulları (BasketItem) arasındakı əlaqə (1-in Çoxa)
        builder.HasMany(b => b.Items)
               .WithOne(i => i.Basket)
               .HasForeignKey(i => i.BasketId)
               .OnDelete(DeleteBehavior.Cascade); // Səbət silinəndə içindəki məhsullar da bazadan silinsin
    }
}