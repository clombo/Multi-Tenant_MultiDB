using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTA.Domain.Entities;

namespace MTA.Data.EntityConfigurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    //TODO configure product entity
    //Keys will be created by app (how to disable on key)
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
            .ToTable("Products");

        builder
            .HasKey(key => key.Id);
        
        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(p => p.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasIndex(i => i.Name)
            .IsUnique();

        builder
            .Property(p => p.Price)
            .IsRequired();
        
    }
}