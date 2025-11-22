using Anazon.Configs;
using Anazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anazon.Database.Config;

public class TagConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    { 
        builder.HasKey(t => t.Key);
    }
}

public class ProductTagConfig : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    { 
        builder.HasKey(pt => new { pt.ProductId, pt.Tag });

        builder.HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId);

        builder.HasOne<Tag>()
            .WithMany()
            .HasForeignKey(pt => pt.Tag);
    }
}