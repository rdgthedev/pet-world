using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class SupplierMap : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Supplier");

        builder.HasKey(supplier => supplier.Id);
        builder.Property(supplier => supplier.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(supplier => supplier.Company)
            .HasColumnName("Company")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(supplier => supplier.CNPJ)
            .HasColumnName("CNPJ")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();
        
        builder.Property(supplier => supplier.Representative)
            .HasColumnName("Representative")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();
        
        builder.Property(supplier => supplier.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();
        
        builder.Property(supplier => supplier.Street)
            .HasColumnName("Street")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(supplier => supplier.Number)
            .HasColumnName("Number")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(supplier => supplier.Neighborhood)
            .HasColumnName("Neighborhood")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(supplier => supplier.Complement)
            .HasColumnName("Complement")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120);
        
        builder.Property(supplier => supplier.City)
            .HasColumnName("City")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(180)
            .IsRequired();
        
        builder.Property(supplier => supplier.State)
            .HasColumnName("State")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(2)
            .IsRequired();
        
        builder.HasIndex(product => product.Id, "IX_Supplier_Id")
            .IsUnique();
    }
}