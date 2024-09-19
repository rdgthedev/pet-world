using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(s => s.SupplierId)
            .HasConstraintName("FK_Product_Supplier_SupplierId")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.SupplierId)
            .HasConstraintName("FK_Product_Category_CategoryId");

        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .HasColumnName("Image")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnName("Price")
            .HasColumnType("MONEY")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired(false);

        builder.HasIndex(s => s.Id, "IX_Product_Id")
            .IsUnique();

        builder.HasIndex(product => product.Name, "IX_Product_Name")
            .IsUnique();
    }
}