using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Data.Mappings;

public class CartItemMap : IEntityTypeConfiguration<Domain.Entities.CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItem");

        builder.HasKey(ci => ci.Id);

        builder.HasOne(ci => ci.Product)
            .WithOne()
            .HasConstraintName("FK_CartItem_Product_ProductId")
            .HasForeignKey<CartItem>(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasConstraintName("FK_CartItem_Cart_CartId")
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // builder.HasOne(ci => ci.Order)
        //     .WithMany(o => o.Items)
        //     .HasConstraintName("FK_CartItem_Order_OrderId")
        //     .HasForeignKey(ci => ci.OrderId)
        //     .IsRequired(false)
        //     .OnDelete(DeleteBehavior.NoAction);

        builder.Property(s => s.Quantity)
            .HasColumnName("Quantity")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(s => s.Price)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();

        builder.Property(s => s.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();

        builder.HasIndex(ci => ci.Id, "IX_CartItem_Id");
        
        builder.HasIndex(ci => new { ci.CartId, ci.ProductId })
            .IsUnique()
            .HasDatabaseName("IX_CartItem_CartId_ProductId");

        builder.HasIndex(ci => ci.ProductId)
            .IsUnique(false) 
            .HasDatabaseName("IX_CartItem_ProductId");
    }
}