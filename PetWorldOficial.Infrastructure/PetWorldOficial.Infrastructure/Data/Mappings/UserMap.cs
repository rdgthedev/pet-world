using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Complement)
            .HasColumnName(nameof(User.Complement))
            .HasColumnType("NVARCHAR")
            .IsRequired(false);

        builder.Property(u => u.LastUpdatedAt)
            .HasColumnName(nameof(User.LastUpdatedAt))
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.HasMany(u => u.Animals)
            .WithOne(u => u.Owner)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(u => u.Schedullings)
            .WithOne(s => s.Employee)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        builder.HasOne(u => u.Cart)
            .WithOne(c => c.Client)
            .HasConstraintName("FK_Cart_User_ClientId")
            .HasForeignKey<Cart>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}