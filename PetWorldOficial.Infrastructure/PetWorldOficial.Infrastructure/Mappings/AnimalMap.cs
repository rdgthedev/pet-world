using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Infrastructure.Mappings;

public class AnimalMap : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("Animal");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();
        
        builder.Property(a => a.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(a => a.Species)
            .HasColumnName("Species")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30)
            .IsRequired();
        
        builder.Property(a => a.Race)
            .HasColumnName("Race")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(a => a.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("DATETIME");

        builder.Property(a => a.Gender)
            .HasColumnName("Gender")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasOne(a => a.User)
            .WithMany(u => u.Animals)
            .HasForeignKey(a => a.UserId)
            .HasConstraintName("FK_Animal_User_UserId")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(a => a.Id, "IX_Animal_Id")
            .IsUnique();
    }
}