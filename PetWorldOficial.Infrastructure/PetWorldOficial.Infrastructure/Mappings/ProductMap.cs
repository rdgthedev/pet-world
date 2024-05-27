using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(product => product.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(180)
            .IsRequired();
        
        builder.Property(product => product.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(product => product.ImageUrl)
            .HasColumnName("Image")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(product => product.Price)
            .HasColumnName("Price")
            .HasColumnType("MONEY")
            .IsRequired();
        
        builder.HasOne(product => product.Supplier)
            .WithMany(supplier => supplier.Products)
            .HasForeignKey(product => product.SupplierId)
            .HasConstraintName("FK_Product_Supplier_SupplierId")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(product => product.Id, "IX_Product_Id")
            .IsUnique();
        
        builder.HasIndex(product => product.Name, "IX_Product_Name")
            .IsUnique();
    }
}