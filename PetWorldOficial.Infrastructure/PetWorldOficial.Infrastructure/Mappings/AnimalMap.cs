using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

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

        builder.HasOne(a => a.Race)
            .WithMany(r => r.Animals)
            .HasConstraintName("FK_Animal_Race_RaceId")
            .HasForeignKey(a => a.RaceId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(a => a.Category)
            .WithMany(r => r.Animals)
            .HasConstraintName("FK_Animal_Category_CategoryId")
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder
            .HasOne(a => a.Owner)
            .WithMany(u => u.Animals)
            .HasForeignKey(a => a.OwnerId)
            .HasConstraintName("FK_Animal_User_UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(a => a.Gender)
            .HasColumnName("Gender")
            .HasConversion(
                eg => eg.ToString(),
                s => (EGender)Enum.Parse(typeof(EGender), s)
            )
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.BirthDate)
            .HasColumnName("BirthDate")
            .HasColumnType("DATE")
            .IsRequired(false);

        builder.Property(a => a.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder.Property(a => a.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired(false);

        builder.HasIndex(a => a.Id, "IX_Animal_Id")
            .IsUnique();

        builder.HasIndex(a => a.Id, "IX_Animal_Name")
            .IsUnique();
    }
}