﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Data.Mappings;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.Property(s => s.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();

        builder.Property(s => s.Code)
            .HasColumnName("Code")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(o => o.PaymentMethod)
            .HasColumnName("PaymentMethod")
            .HasConversion(
                pm => pm.ToString(),
                pm => (EPaymentMethod)Enum.Parse(typeof(EPaymentMethod), pm)
            )
            .IsRequired();

        builder.Property(s => s.PaymentDate)
            .HasColumnName("PaymentDate")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.Property(o => o.Status)
            .HasColumnName("Status")
            .HasConversion(
                os => os.ToString(),
                os => (EOrderStatus)Enum.Parse(typeof(EOrderStatus), os)
            )
            .IsRequired();

        builder.Property(o => o.StripeSessionId)
            .HasColumnName("StripeSessionId")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(128)
            .IsRequired(false);
        
     
        builder.HasMany(o => o.Items) 
            .WithOne(ci => ci.Order) 
            .HasConstraintName("FK_OrderItem_Order_OrderId")
            .HasForeignKey(ci => ci.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(o => o.Id, "IX_Order_Id");
        builder.HasIndex(o => o.Code, "IX_Order_Code");
    }
}