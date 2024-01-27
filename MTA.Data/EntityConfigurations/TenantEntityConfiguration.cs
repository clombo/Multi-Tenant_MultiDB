using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTA.Domain.Entities;

namespace MTA.Data.EntityConfigurations;

public class TenantEntityConfiguration : IEntityTypeConfiguration<TenantEntity>
{
    public void Configure(EntityTypeBuilder<TenantEntity> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(k => k.Id);

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
            .Property(p => p.ConnectionString)
            .IsRequired();
    }
}