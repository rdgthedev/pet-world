using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Infrastructure.Data.Mappings;

public class SchedullingMap : IEntityTypeConfiguration<Schedulling>
{
    public void Configure(EntityTypeBuilder<Schedulling> builder)
    {
        builder.ToTable("Schedulling");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(s => s.Animal)
            .WithMany(a => a.Schedullings)
            .HasConstraintName("FK_Schedulling_Animal_AnimalId")
            .HasForeignKey(s => s.AnimalId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(s => s.Service)
            .WithMany(s => s.Schedullings)
            .HasConstraintName("FK_Schedulling_Service_ServiceId")
            .HasForeignKey(s => s.ServiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(s => s.Employee)
            .WithMany(s => s.Schedullings)
            .HasConstraintName("FK_Schedulling_User_EmployeeId")
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // builder.HasOne(s => s.Category)
        //     .WithMany(a => a.Schedullings)
        //     .HasConstraintName("FK_Schedulling_Category_CategoryId")
        //     .HasForeignKey(s => s.CategoryId)
        //     .OnDelete(DeleteBehavior.Restrict)
        //     .IsRequired();

        builder.Property(s => s.Code)
            .HasColumnName("Code")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(s => s.Date)
            .HasColumnName("Date")
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(s => s.Time)
            .HasColumnName("Time")
            .HasColumnType("TIME")
            .IsRequired();

        builder.Property(s => s.Observation)
            .HasColumnName("Observation")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(128)
            .IsRequired(false);

        builder.Property(s => s.Status)
            .HasColumnName("Status")
            .HasConversion(
                v => v.ToString(),
                v => (ESchedullingStatus)Enum.Parse(typeof(ESchedullingStatus), v)
            )
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(s => s.LastUpdatedAt)
            .HasColumnName("LastUpdatedAt")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.HasIndex(s => s.Id, "IX_Schedulling_Id");
        builder.HasIndex(s => s.EmployeeId, "IX_Schedulling_EmployeeId");
    }
}