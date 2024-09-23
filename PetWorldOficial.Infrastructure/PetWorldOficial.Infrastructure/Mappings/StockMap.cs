using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class StockMap : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stock");

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(s => s.Product)
            .WithOne(p => p.Stock)
            .HasConstraintName("FK_Stock_ProductId")
            .HasForeignKey<Stock>(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(s => s.Quantity)
            .HasColumnName("Quantity")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.HasIndex(s => s.Id, "IX_Stock_Id");
        builder.HasIndex(s => s.Id, "IX_Stock_ProductId");
    }
}