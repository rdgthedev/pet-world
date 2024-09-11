using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Mappings;

public class SupplierMap : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Supplier");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(s => s.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(140)
            .IsRequired();

        builder.Property(s => s.CNPJ)
            .HasColumnName("CNPJ")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(s => s.CellPhone)
            .HasColumnName("CellPhone")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(supplier => supplier.Street)
            .HasColumnName("Street")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(s => s.Number)
            .HasColumnName("Number")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(s => s.Neighborhood)
            .HasColumnName("Neighborhood")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(s => s.Complement)
            .HasColumnName("Complement")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120);

        builder.Property(s => s.City)
            .HasColumnName("City")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(s => s.State)
            .HasColumnName("State")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(s => s.Status)
            .HasColumnName("Status")
            .HasConversion(
                eas => eas.ToString(),
                eas => (EActivingStatus)Enum.Parse(typeof(EActivingStatus), eas)
            )
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME");

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .IsRequired();
    }
}