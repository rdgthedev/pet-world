using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Data.Mappings;

public class CartMap : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(c => c.Client)
            .WithOne()
            .HasConstraintName("FK_Cart_User_ClientId")
            .HasForeignKey<Cart>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME")
            .IsRequired()
            .IsRequired(false);

        builder.Property(c => c.ExpiresDate)
            .HasColumnName("ExpiresDate")
            .HasColumnType("DATETIME")
            .IsRequired();
        
        builder.Property(s => s.SubTotalPrice)
            .HasColumnName("SubTotalPrice")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();

        builder.Property(s => s.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();
    }
}