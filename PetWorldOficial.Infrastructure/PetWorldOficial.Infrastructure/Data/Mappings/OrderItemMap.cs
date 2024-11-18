using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Data.Mappings;

public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasKey(ci => ci.Id);

        builder.HasOne(ci => ci.Product)
            .WithOne()
            .HasConstraintName("FK_OrderItem_Product_ProductId")
            .HasForeignKey<OrderItem>(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // builder.HasOne(ci => ci.Cart)
        //     .WithMany(c => c.Items)
        //     .HasConstraintName("FK_CartItem_Cart_CartId")
        //     .HasForeignKey(ci => ci.CartId)
        //     .OnDelete(DeleteBehavior.Cascade)
        //     .IsRequired(false);

        builder.HasOne(ci => ci.Order)
            .WithMany(o => o.Items)
            .HasConstraintName("FK_OrderItem_Order_OrderId")
            .HasForeignKey(ci => ci.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

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

        builder.HasIndex(ci => ci.Id, "IX_OrderItem_Id");
        
        builder.HasIndex(ci => new { ci.OrderId, ci.ProductId })
            .IsUnique()
            .HasDatabaseName("IX_OrderItem_OrderId_ProductId");

        builder.HasIndex(ci => ci.ProductId)
            .IsUnique(false) 
            .HasDatabaseName("IX_OrderItem_ProductId");
    }
}